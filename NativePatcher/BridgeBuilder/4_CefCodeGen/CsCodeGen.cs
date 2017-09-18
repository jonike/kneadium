﻿//MIT, 2016-2017 ,WinterDev
using System;
using System.Collections.Generic;
using System.Text;
namespace BridgeBuilder
{


    class CsCallToNativeCodeGen : CsCodeGen
    {
        //generate cs code
        //call from cs to native side
        //------------------------------


        TypePlan _typeTxInfo;
        CodeTypeDeclaration _orgDecl;
        internal static void PrepareCsMetArg(MethodParameter par, string argName)
        {
            par.ClearExtractCode();
            TypeSymbol typeSymbol = par.TypeSymbol;
            TypeBridgeInfo bridge = typeSymbol.BridgeInfo;

            //check if parTx.Name is keyword?
            switch (par.Name)
            {
                case "event":
                    par.Name = "_event";
                    break;
                case "checked":
                    par.Name = "_checked";
                    break;
                case "object":
                    par.Name = "_object";
                    break;

            }
            //-----------------

            switch (typeSymbol.TypeSymbolKind)
            {
                default:

                    break;
                case TypeSymbolKind.Simple:
                    {
                        SimpleTypeSymbol simpleType = (SimpleTypeSymbol)typeSymbol;
                        if (simpleType.IsEnum)
                        {
                            //.net send enum as int32 
                            par.ArgExtractCode = argName + ".I32=(int)" + par.Name;
                        }
                        else
                        {
                            switch (simpleType.PrimitiveTypeKind)
                            {
                                default:
                                    break;
                                case PrimitiveTypeKind.size_t: //uint32                                     
                                    par.ArgExtractCode = argName + ".I32 = (int)" + par.Name;
                                    break;
                                case PrimitiveTypeKind.Bool:
                                    par.ArgExtractCode = argName + ".I32= " + par.Name + "?1:0";//review here
                                    break;
                                case PrimitiveTypeKind.NotPrimitiveType:
                                    par.ArgExtractCode = "(" + simpleType.ToString() + "*)" + argName + "->" + bridge.CefCppSlotName;
                                    break;
                                case PrimitiveTypeKind.NaitveInt:
                                    par.ArgExtractCode = argName + ".I32= (int)" + par.Name;//review here
                                    break;
                                case PrimitiveTypeKind.Int32:
                                    par.ArgExtractCode = argName + ".I32= " + par.Name;//review here 
                                    break;
                                case PrimitiveTypeKind.UInt32:
                                    {
                                        par.ArgExtractCode = argName + ".I32= (int)" + par.Name;//review here
                                    }
                                    break;
                                case PrimitiveTypeKind.Int64:
                                    {
                                        par.ArgExtractCode = argName + ".I64= " + par.Name;//review here
                                    }
                                    break;
                                case PrimitiveTypeKind.Float:
                                case PrimitiveTypeKind.Double:
                                    par.ArgExtractCode = argName + ".Num = " + par.Name;
                                    break;
                            }
                        }
                    }
                    break;
                case TypeSymbolKind.TypeDef:
                    {
                        //eg. FileDialogMode
                        //check refer to 
                        //eg CefProcessId
                        CTypeDefTypeSymbol ctypedef = (CTypeDefTypeSymbol)typeSymbol;
                        TypeSymbol referTo = ctypedef.ReferToTypeSymbol;
                        if (referTo.TypeSymbolKind == TypeSymbolKind.Simple)
                        {
                            SimpleTypeSymbol ss = (SimpleTypeSymbol)referTo;
                            if (ss.IsEnum)
                            {
                                if (ctypedef.ParentType != null && !ctypedef.ParentType.IsGlobalCompilationUnitTypeDefinition)
                                {
                                    par.ArgExtractCode = argName + ".I32 = (int)" + par.Name;
                                }
                                else
                                {
                                    par.ArgExtractCode = argName + ".I32 = (int)" + par.Name;
                                }

                            }
                            else if (ss.PrimitiveTypeKind == PrimitiveTypeKind.NotPrimitiveType)
                            {


                            }
                            else if (ss.PrimitiveTypeKind == PrimitiveTypeKind.UInt32)
                            {
                                //cef_color_t
                                par.ArgExtractCode = argName + ".I32 = (int)" + par.Name;
                            }
                            else
                            {
                                par.ArgExtractCode = argName + ".I32 = (int)" + par.Name;
                            }
                        }
                        else
                        {

                        }

                    }
                    break;
                case TypeSymbolKind.ReferenceOrPointer:
                    {
                        ReferenceOrPointerTypeSymbol refOrPtr = (ReferenceOrPointerTypeSymbol)typeSymbol;
                        switch (refOrPtr.Kind)
                        {
                            default:
                                break;
                            case ContainerTypeKind.Pointer:
                                {
                                    TypeBridgeInfo bridgeInfo = refOrPtr.BridgeInfo;
                                    TypeSymbol elemtType = refOrPtr.ElementType;
                                    if (elemtType is SimpleTypeSymbol)
                                    {
                                        SimpleTypeSymbol ss = (SimpleTypeSymbol)elemtType;
                                        string elem_typename = ss.ToString();
                                        switch (elem_typename)
                                        {
                                            default:
                                                {

                                                }
                                                break;
                                            case "void":
                                                {
                                                    //void* 
                                                    par.ArgExtractCode = argName + ".Ptr=" + par.Name;
                                                }
                                                break;
                                            case "char":
                                                {
                                                    //char* 
                                                    par.ArgExtractCode = argName + ".Ptr=" + par.Name;
                                                }
                                                break;
                                        }
                                    }
                                    else
                                    {

                                    }
                                }
                                break;
                            case ContainerTypeKind.CefRefPtr:
                                {
                                    //from cef 'smart' pointer

                                    TypeBridgeInfo bridgeInfo = refOrPtr.BridgeInfo;
                                    TypeSymbol elemtType = refOrPtr.ElementType;
                                    if (elemtType is SimpleTypeSymbol)
                                    {
                                        CefTypeTx txplan = ((SimpleTypeSymbol)elemtType).CefTxPlan;
                                        if (txplan == null)
                                        {
                                            if (elemtType.ToString() == "CefBaseRefCounted")
                                            {
                                                //bool SetUserData(CefRefPtr<CefBaseRefCounted> user_data)
                                                //only 1 
                                                par.ArgExtractCode = argName + ".Ptr=" + par.Name + ".nativePtr";
                                            }
                                            else
                                            {
                                                throw new NotSupportedException();
                                            }
                                        }
                                        else
                                        {
                                            CodeTypeDeclaration implTypeDecl = txplan.ImplTypeDecl;
                                            ImplWrapDirection implWrapDirection = ImplWrapDirection.None;
                                            if (implTypeDecl.Name.Contains("CToCpp"))
                                            {
                                                par.ArgExtractCode = argName + ".Ptr=" + par.Name + ".nativePtr";
                                            }
                                            else if (implTypeDecl.Name.Contains("CppToC"))
                                            {
                                                par.ArgExtractCode = argName + ".Ptr=" + par.Name + ".nativePtr";
                                            }
                                            else
                                            {
                                                implWrapDirection = ImplWrapDirection.None;
                                                string met = CefTypeTx.GetSmartPointerMet(implWrapDirection);
                                                string slotName = bridge.CefCppSlotName.ToString();
                                                par.ArgExtractCode = implTypeDecl.Name + "::" + met + "(" + (argName + "->" + slotName) + ")";

                                            }
                                        }
                                    }
                                    else
                                    {
                                        //should not visit here
                                        throw new NotSupportedException();
                                    }
                                }
                                break;
                            case ContainerTypeKind.ByRef:
                                {
                                    TypeSymbol elemType = refOrPtr.ElementType;
                                    switch (elemType.TypeSymbolKind)
                                    {
                                        default:
                                            break;
                                        case TypeSymbolKind.Simple:
                                            {

                                                string elem_typename = refOrPtr.ElementType.ToString();
                                                switch (elem_typename)
                                                {
                                                    default:
                                                        break;
                                                    case "bool"://bool&
                                                        {
                                                            //eg. bool GetAccelerator(int command_id,int& key_code,bool& shift_pressed,bool& ctrl_pressed,bool& alt_pressed)


                                                            //string slotName = bridge.CefCppSlotName.ToString();
                                                            //par.ArgExtractCode = "*((bool*)" + argName + "->" + slotName + ")";

                                                            //par.ArgExtractCode = "&tmp_" + argName;
                                                            //if (!par.IsConst)
                                                            //{
                                                            //    par.ArgPreExtractCode = elem_typename + " tmp_" + argName + "=" + argName + "->" + slotName;
                                                            //    par.ArgPostExtractCode = PrepareCppReturnToCs(par.TypeSymbol, argName, " tmp_" + argName);
                                                            //}
                                                            par.ArgExtractCode = argName + ".I32=" + "(" + par.Name + "?1:0)";
                                                            par.ArgPostExtractCode = par.Name + "= " + argName + ".I32 != 0;"; //restore result back
                                                        }
                                                        break;
                                                    case "size_t":
                                                        {
                                                            //size_t&
                                                            //bool GetDataResource(int resource_id, void*&data,size_t & data_size)
                                                            //bool GetDataResourceForScale(int resource_id,ScaleFactor scale_factor,void*& data,size_t& data_size)

                                                            //string slotName = bridge.CefCppSlotName.ToString();

                                                            //par.ArgExtractCode = "&tmp_" + argName;
                                                            //if (!par.IsConst)
                                                            //{
                                                            //    par.ArgPreExtractCode = elem_typename + " tmp_" + argName + "=" + argName + "->" + slotName;
                                                            //    par.ArgPostExtractCode = PrepareCppReturnToCs(par.TypeSymbol, argName, " tmp_" + argName);
                                                            //}
                                                            //string slotName = bridge.CefCppSlotName.ToString();
                                                            //par.ArgExtractCode = "*((size_t*)" + argName + "->" + slotName + ")";

                                                            par.ArgExtractCode = argName + ".I32= (int)" + par.Name;
                                                            par.ArgPostExtractCode = par.Name + "= (uint)" + argName + ".I32;"; //restore result back
                                                        }
                                                        break;
                                                    case "float": //float&
                                                        {
                                                            //eg. bool GetRepresentationInfo(float scale_factor,float& actual_scale_factor,int& pixel_width,int& pixel_height)

                                                            //string slotName = bridge.CefCppSlotName.ToString();

                                                            //par.ArgExtractCode = "&tmp_" + argName;
                                                            //if (!par.IsConst)
                                                            //{
                                                            //    par.ArgPreExtractCode = elem_typename + " tmp_" + argName + "=" + argName + "->" + slotName;
                                                            //    par.ArgPostExtractCode = PrepareCppReturnToCs(par.TypeSymbol, argName, " tmp_" + argName);
                                                            //}
                                                            //string slotName = bridge.CefCppSlotName.ToString();
                                                            //par.ArgExtractCode = "*((float*)" + argName + "->" + slotName + ")";

                                                            par.ArgExtractCode = argName + ".Num=" + par.Name;
                                                            par.ArgPostExtractCode = par.Name + "= (float)" + argName + ".Num;"; //restore result back

                                                        }
                                                        break;
                                                    case "int":
                                                        {
                                                            //eg .bool GetRepresentationInfo(float scale_factor,float& actual_scale_factor,int& pixel_width,int& pixel_height)

                                                            //string slotName = bridge.CefCppSlotName.ToString();

                                                            //par.ArgExtractCode = "&tmp_" + argName;
                                                            //if (!par.IsConst)
                                                            //{
                                                            //    par.ArgPreExtractCode = elem_typename + " tmp_" + argName + "=" + argName + "->" + slotName;
                                                            //    par.ArgPostExtractCode = PrepareCppReturnToCs(par.TypeSymbol, argName, " tmp_" + argName);
                                                            //}
                                                            //string slotName = bridge.CefCppSlotName.ToString();
                                                            //par.ArgExtractCode = "*((int*)" + argName + "->" + slotName + ")";

                                                            //--------------------------------------------------------------------------
                                                            //bring data

                                                            par.ArgExtractCode = argName + ".I32=" + par.Name;
                                                            par.ArgPostExtractCode = par.Name + "= (int)" + argName + ".I32;"; //restore result back
                                                        }
                                                        break;
                                                    case "CefWindowInfo":
                                                    case "CefPoint":
                                                    case "CefSize":
                                                    case "CefRect":
                                                    case "CefRange":
                                                        {

                                                            //create struct at native part?
                                                            //****


                                                            //eg. void ShowDevTools(const CefWindowInfo& windowInfo,CefRefPtr<CefClient> client,const CefBrowserSettings& settings,const CefPoint& inspect_element_at)
                                                            //eg. void ImeSetComposition(const CefString& text,const std::vector<CefCompositionUnderline>& underlines,const CefRange& replacement_range,const CefRange& selection_range)

                                                            //string slotName = bridge.CefCppSlotName.ToString();
                                                            //par.ArgExtractCode = "&tmp_" + argName;

                                                            //if (!par.IsConst)
                                                            //{
                                                            //    par.ArgPreExtractCode = elem_typename + "* tmp_" + argName + "=" + "(*" + elem_typename + ")" + argName + "->" + slotName;
                                                            //    par.ArgPostExtractCode = PrepareCppReturnToCs(par.TypeSymbol, argName, " tmp_" + argName);
                                                            //}
                                                            //string slotName = bridge.CefCppSlotName.ToString();
                                                            //par.ArgExtractCode = "*((" + elem_typename + "*)" + argName + "->" + slotName + ")";
                                                            par.ArgExtractCode = argName + ".Ptr=" + par.Name + ".nativePtr";
                                                        }
                                                        break;
                                                    case "CefString":
                                                        {
                                                            //native need cefstring
                                                            //so we create a cef string handle holder
                                                            par.ArgPreExtractCode = argName + ".Ptr=" + " Cef3Binder.MyCefCreateStringHolder(" + par.Name + ");\r\n";
                                                            par.ArgPostExtractCode = "Cef3Binder.MyCefDeletePtr(" + argName + ".Ptr);";
                                                        }
                                                        break;
                                                }
                                            }
                                            break;
                                        case TypeSymbolKind.ReferenceOrPointer:
                                            {
                                                string elem_typename = refOrPtr.ElementType.ToString();
                                                switch (elem_typename)
                                                {
                                                    default:

                                                        break;
                                                    case "void*":
                                                        {
                                                            //eg. bool GetDataResource(int resource_id,void*& data,size_t& data_size)
                                                            //string slotName = bridge.CefCppSlotName.ToString();

                                                            //par.ArgExtractCode = "&tmp_" + argName;
                                                            //if (par.IsConst)
                                                            //{
                                                            //    par.ArgPreExtractCode = elem_typename + " tmp_" + argName + "=" + "(*" + elem_typename + ")" + argName + "->" + slotName;
                                                            //}
                                                            par.ArgExtractCode = argName + ".Ptr=" + par.Name;
                                                            par.ArgPostExtractCode = par.Name + "= " + argName + ".Ptr;"; //restore result back
                                                        }
                                                        break;
                                                    case "CefRefPtr<CefV8Value>":
                                                        {
                                                            //eg. bool Eval(const CefString& code,const CefString& script_url,int start_line,CefRefPtr<CefV8Value>& retval,CefRefPtr<CefV8Exception>& exception)
                                                            //if (par.IsConst)
                                                            //{

                                                            //}
                                                            par.ArgExtractCode = argName + ".Ptr=" + par.Name;
                                                            par.ArgPostExtractCode = par.Name + "= " + argName + ".Ptr;"; //restore result back
                                                        }
                                                        break;
                                                    case "CefRefPtr<CefV8Exception>":
                                                        {
                                                            //eg. bool Eval(const CefString& code,const CefString& script_url,int start_line,CefRefPtr<CefV8Value>& retval,CefRefPtr<CefV8Exception>& exception)
                                                            //if (par.IsConst)
                                                            //{

                                                            //}
                                                            par.ArgExtractCode = argName + ".Ptr=" + par.Name;
                                                            par.ArgPostExtractCode = par.Name + "= " + argName + ".Ptr;"; //restore result back
                                                        }
                                                        break;
                                                }
                                            }
                                            break;
                                        case TypeSymbolKind.Vec:
                                            {
                                                //for cef,...
                                                //list of what

                                                string elem_typename = refOrPtr.ElementType.ToString();
                                                switch (elem_typename)
                                                {
                                                    default:
                                                        throw new NotFiniteNumberException();
                                                    case "std::vector<int64>":
                                                        par.ArgExtractCode = argName + ".Ptr=Cef3Binder.CreateStdList(1)";
                                                        par.ArgPostExtractCode = "Cef3Binder.CopyStdInt64ListAndDestroyNativeSide(" + argName + ".Ptr," + par.Name + ");";

                                                        break;
                                                    case "std::vector<CefString>":
                                                        par.ArgExtractCode = argName + ".Ptr=Cef3Binder.CreateStdList(2)";
                                                        par.ArgPostExtractCode = "Cef3Binder.CopyStdStringListAndDestroyNativeSide(" + argName + ".Ptr," + par.Name + ");";
                                                        break;
                                                    case "std::vector<CefCompositionUnderline>":
                                                        par.ArgExtractCode = argName + ".Ptr=Cef3Binder.CreateStdList(3);";
                                                        break;
                                                }

                                            }
                                            break;
                                        case TypeSymbolKind.Template:
                                            break;
                                        case TypeSymbolKind.TypeDef:
                                            {
                                                //eg. void ImeSetComposition(const CefString& text,const std::vector<CefCompositionUnderline>& underlines,const CefRange& replacement_range,const CefRange& selection_range)
                                                CTypeDefTypeSymbol ctypedef = (CTypeDefTypeSymbol)elemType;
                                                par.ArgExtractCode = argName + ".Ptr= " + par.Name + ".nativePtr";

                                            }
                                            break;

                                    }
                                }
                                break;
                        }
                    }
                    break;
            }

        }

        public void GenerateCsCode(
            CefTypeTx txplan,
            CodeTypeDeclaration orgDecl,
            CodeTypeDeclaration implTypeDecl,
            bool withNewMethod,
            CodeStringBuilder stbuilder)
        {

            //-----------------------------------------------------------------------
            _orgDecl = orgDecl;
            _typeTxInfo = implTypeDecl.TypePlan;//tx = tye


            int j = _typeTxInfo.methods.Count;
            //-----------------------------------------------------------------------
            CodeStringBuilder csStruct = new CodeStringBuilder();
            int maxPar = 0;
            CodeGenUtils.AddComment(orgDecl.LineComments, csStruct);
            //
            csStruct.AppendLine("public struct " + orgDecl.Name + "{");
            csStruct.AppendLine("const int _typeNAME=" + orgDecl.TypePlan.CsInterOpTypeNameId + ";");
            string releaseMetName = orgDecl.Name + "_Release_0";
            csStruct.AppendLine("const int " + releaseMetName + "= (_typeNAME <<16) | 0;");

            for (int i = 0; i < j; ++i)
            {
                MethodPlan metTx = _typeTxInfo.methods[i];
                if (metTx.pars.Count > maxPar)
                {
                    maxPar = metTx.pars.Count;
                }
                if (metTx.CppMethodSwitchCaseName == null)
                {
                    metTx.CppMethodSwitchCaseName = implTypeDecl.Name + "_" + metTx.Name;
                    //throw new NotSupportedException();
                }
                csStruct.AppendLine("const int " + metTx.CppMethodSwitchCaseName + "= (_typeNAME <<16) |" + (i + 1) + ";");
            }
            //-----------------------------------------------------------------------
            //create ctor
            csStruct.AppendLine("internal IntPtr nativePtr;");
            csStruct.AppendLine("internal " + orgDecl.Name + "(IntPtr nativePtr){");
            csStruct.AppendLine("this.nativePtr= nativePtr;");
            csStruct.AppendLine("}");
            //-----------------------------------------------------------------------
            //release method for cef instance object
            csStruct.AppendLine("public void Release(){");
            csStruct.AppendLine("JsValue ret;");
            csStruct.AppendLine("Cef3Binder.MyCefMet_Call0(this.nativePtr, " + releaseMetName + ", out ret);");
            csStruct.AppendLine("this.nativePtr= IntPtr.Zero;");
            csStruct.AppendLine("}");

            //-----------------------------------------------------------------------
            for (int i = 0; i < j; ++i)
            {
                CodeStringBuilder met_stbuilder = new CodeStringBuilder();

                MethodPlan metTx = _typeTxInfo.methods[i];
                GenerateCsMethod(metTx, met_stbuilder);
                csStruct.Append(met_stbuilder.ToString());
            }
            //-----------------------------------------------------------------------
            if (withNewMethod && txplan.CppImplClassName != null)
            {
                csStruct.AppendLine("public static " + orgDecl.Name + " New(MyCefCallback callback){");
                csStruct.AppendLine("JsValue not_used= new JsValue();");
                csStruct.AppendLine("return new " + orgDecl.Name + "(Cef3Binder.NewInstance(_typeNAME,callback,ref not_used));");
                csStruct.AppendLine("}");
            }
            //-----------------------------------------------------------------------
            csStruct.AppendLine("}");  //close struct 
            //add to stbuilder
            stbuilder.Append(csStruct.ToString());

        }
        void GenerateCsMethod(MethodPlan met, CodeStringBuilder stbuilder)
        {

            if (met.CsLeftMethodBodyBlank) return;  //temp here 
            //---------------------------------------
            //extract managed args and then call native c++ method 
            MethodParameter ret = met.ReturnPlan;
            List<MethodParameter> pars = met.pars;
            int parCount = pars.Count;
            if (parCount > 15)
            {
                throw new NotSupportedException();
            }
            //--------------------------- 
            //generate method sig 
            //--------------------------- 

            stbuilder.Append(
                "\r\n" +
                "// gen! " + met.ToString() + "\r\n"
                );
            //---------------------------
            CodeGenUtils.AddComment(met.metDecl.LineComments, stbuilder);

            for (int i = 0; i < parCount; ++i)
            {
                //prepare some method args
                //get pars from parameter .                 
                PrepareCsMetArg(pars[i], "v" + (i + 1));
            }

            ret.ArgExtractCode = CefTypeTx.PrepareDataFromNativeToCs(ret.TypeSymbol, "ret", "ret_result");
            stbuilder.AppendLine();
            //------------------
            stbuilder.Append("public ");
            stbuilder.Append(CefTypeTx.GetCsRetName(ret.TypeSymbol));
            stbuilder.Append(" ");

            //some method name should be renamed
            if (met.Name == "GetType")
            {
                stbuilder.Append("_" + met.Name);
            }
            else
            {
                stbuilder.Append(met.Name);
            }
            stbuilder.Append("(");
            //---------------------------

            for (int i = 0; i < parCount; ++i)
            {
                if (i > 0)
                {
                    stbuilder.AppendLine(",");
                }
                MethodParameter parTx = pars[i];
                stbuilder.Append(CefTypeTx.GetCsRetName(parTx.TypeSymbol));
                stbuilder.Append(" ");
                stbuilder.Append(parTx.Name);
            }
            stbuilder.Append(")");
            stbuilder.AppendLine("{");


            StringBuilder argList = new StringBuilder();
            for (int i = 0; i < parCount; ++i)
            {
                argList.AppendLine("JsValue " + "v" + (i + 1) + "=new JsValue();");
            }
            argList.AppendLine("JsValue ret;");
            stbuilder.Append(argList.ToString());

            //---------------------------
            for (int i = 0; i < parCount; ++i)
            {
                MethodParameter parTx = pars[i];
                if (!string.IsNullOrEmpty(parTx.ArgPreExtractCode))
                {
                    stbuilder.Append(parTx.ArgPreExtractCode);
                }
            }
            //---------------------------
            for (int i = 0; i < parCount; ++i)
            {
                MethodParameter parTx = pars[i];
                if (!string.IsNullOrEmpty(parTx.ArgExtractCode))
                {
                    stbuilder.Append(parTx.ArgExtractCode);
                    stbuilder.AppendLine(";");
                }
            }
            string orgDeclName = _orgDecl.Name;
            stbuilder.AppendLine();//marker

            stbuilder.Append("Cef3Binder.MyCefMet_Call" + parCount + "(");
            stbuilder.Append("this.nativePtr," + met.CppMethodSwitchCaseName + ",out ret");
            for (int i = 0; i < parCount; ++i)
            {
                stbuilder.Append(",ref " + "v" + (i + 1));
            }
            stbuilder.Append(");\r\n");


            //clean up input arg
            //--------------------
            for (int i = 0; i < parCount; ++i)
            {
                MethodParameter parTx = pars[i];
                if (parTx.ArgPostExtractCode != null)
                {
                    stbuilder.AppendLine(parTx.ArgPostExtractCode);
                }
            }
            //--------------------

            stbuilder.AppendLine(ret.ArgExtractCode);
            //if (!met.ReturnPlan.IsVoid)
            //{
            //    stbuilder.AppendLine("return ret_result;");
            //}

            stbuilder.AppendLine("}");
        }

    }

    class CsNativeHandlerSwitchTableCodeGen : CsCodeGen
    {
        public void GenerateCefNativeRequestHandlers(List<CefHandlerTx> handlerPlans, StringBuilder stbuilder)
        {
            CodeStringBuilder cef_NativeReqHandlers_Class = new CodeStringBuilder();
            cef_NativeReqHandlers_Class.AppendLine("//------ common cef handler swicth table---------");
            cef_NativeReqHandlers_Class.AppendLine("public static class CefNativeRequestHandlers{");
            cef_NativeReqHandlers_Class.AppendLine("public static void HandleNativeReq(object inst, int met_id,IntPtr args){");
            cef_NativeReqHandlers_Class.AppendLine("switch((met_id>>16)){");
            foreach (CefHandlerTx tx in handlerPlans)
            {
                cef_NativeReqHandlers_Class.AppendLine("case " + tx.OriginalDecl.Name + "._typeNAME:{");
                cef_NativeReqHandlers_Class.AppendLine(tx.OriginalDecl.Name + ".HandleNativeReq(inst as " + tx.OriginalDecl.Name + ".I0," +
                        " inst as " + tx.OriginalDecl.Name + ".I1,met_id,args);");
                cef_NativeReqHandlers_Class.AppendLine("}break;");
            }
            //--------
            //create handle common switch table
            cef_NativeReqHandlers_Class.AppendLine("}");//switch
            cef_NativeReqHandlers_Class.AppendLine("}");//HandleNativeReq()
            cef_NativeReqHandlers_Class.AppendLine("}");

            //add to cs code
            stbuilder.Append(cef_NativeReqHandlers_Class.ToString());
        }
    }

    class CsStructModuleCodeGen : CsCodeGen
    {
        void PrepareCsMetPars(MethodPlan met)
        {
            int j = met.pars.Count;
            for (int i = 0; i < j; ++i)
            {
                MethodParameter parTx = met.pars[i];
                switch (parTx.Name)
                {
                    case "params":
                        {
                            parTx.Name = "_params";
                        }
                        break;
                    case "string":
                        {
                            parTx.Name = "_string";
                        }
                        break;
                    case "object":
                        {
                            parTx.Name = "_object";
                        }
                        break;
                    case "event":
                        {
                            parTx.Name = "_event";
                        }
                        break;
                }
            }

        }
        public void GenerateCsStructClass(CodeTypeDeclaration orgDecl, List<MethodPlan> callToDotNetMets, CodeStringBuilder stbuilder)
        {

            int nn = callToDotNetMets.Count;
            //create interface for this handler
            //we provide 2 interfaces
            //1. singles arg interface
            //2. expanded args interface 
            string className = orgDecl.Name;
            //create a cpp class              
            stbuilder.Append("public struct " + className);
            stbuilder.AppendLine("{");
            stbuilder.AppendLine("public const int _typeNAME=" + orgDecl.TypePlan.CsInterOpTypeNameId + ";");


            for (int mm = 0; mm < nn; ++mm)
            {
                //implement on event notificationi
                MethodPlan met = callToDotNetMets[mm];
                PrepareCsMetPars(met);
                stbuilder.AppendLine("const int " + met.CppMethodSwitchCaseName + "= " + (mm + 1) + ";");
            }


            for (int mm = 0; mm < nn; ++mm)
            {
                //implement on event notificationi
                MethodPlan met = callToDotNetMets[mm];
                //prepare data and call the callback                
                stbuilder.AppendLine("//gen! " + met.metDecl.ToString());
                //
                //GenerateCsExpandedArgsMethodImpl(met, stbuilder);
                string argClassName = GenerateCsMethodArgsClass_JsSlot(met, stbuilder);
                CodeStringBuilder st2 = new CodeStringBuilder();
                GenerateCsMethodArgsClass_Native(met, st2);

                stbuilder.Append(st2.ToString());
                met.CsArgClassName = argClassName;
                //GenerateCsSingleArgMethodImpl(argClassName, met, stbuilder);                  
            }

            //------------------------------            
            stbuilder.Append("public interface I0");
            stbuilder.AppendLine("{");
            for (int mm = 0; mm < nn; ++mm)
            {
                //implement on event notificationi
                MethodPlan met = callToDotNetMets[mm];
                GenerateCsSingleArgMethodImplForInterface(met.CsArgClassName, met, stbuilder);
            }
            stbuilder.AppendLine("}");
            //-----------------

            stbuilder.Append("public interface I1");
            stbuilder.AppendLine("{");
            for (int mm = 0; mm < nn; ++mm)
            {
                //implement on event notificationi
                MethodPlan met = callToDotNetMets[mm];
                GenerateCsExpandedArgsMethodForInterface(met, stbuilder);
            }
            stbuilder.AppendLine("}");
            //-----------------


            stbuilder.AppendLine("public static void HandleNativeReq(I0 i0, I1 i1, int met_id, IntPtr nativeArgPtr)");
            stbuilder.AppendLine("{");
            stbuilder.AppendLine("int met_name = met_id & 0xffff;");
            stbuilder.AppendLine("switch (met_name){");
            //
            for (int mm = 0; mm < nn; ++mm)
            {
                //implement on event notificationi
                MethodPlan met = callToDotNetMets[mm];
                stbuilder.AppendLine("case " + met.CppMethodSwitchCaseName + ":{");
                stbuilder.AppendLine("var args=new " + met.CsArgClassName + "(nativeArgPtr);");
                //i0
                stbuilder.AppendLine("if(i0 != null){");
                stbuilder.AppendLine("i0." + met.Name + "(args);");
                stbuilder.AppendLine("}");
                //i1 expand interface
                stbuilder.AppendLine("if(i1 != null){");
                GenerateCsExpandMethodContent(met, stbuilder);
                stbuilder.AppendLine("}");
                stbuilder.AppendLine("}break;");//case 
            }

            stbuilder.AppendLine("}"); //end switch
            stbuilder.AppendLine("}"); //end method

            //-----------------
            //expansion version for i1

            for (int mm = 0; mm < nn; ++mm)
            {
                //implement on event notificationi
                MethodPlan met = callToDotNetMets[mm];
                GenerateCsSingleArgMethodImplForI1(met.CsArgClassName, met, stbuilder);
            } 
            stbuilder.AppendLine("}"); //end class
        } 
         
        void GenerateCsSingleArgMethodImplForI1(string argClassName, MethodPlan met, CodeStringBuilder stbuilder)
        {
            CodeMethodDeclaration metDecl = (CodeMethodDeclaration)met.metDecl;
            //temp 
            List<MethodParameter> pars = met.pars;
            stbuilder.Append("public static void");
            stbuilder.Append(" ");
            stbuilder.Append(met.Name);
            stbuilder.Append("(I1 i1,");
            stbuilder.Append(argClassName + " args");
            stbuilder.AppendLine("){");
            GenerateCsExpandMethodContent(met, stbuilder);
            stbuilder.AppendLine("}"); //method
        } 
        void GenerateCsExpandMethodContent(MethodPlan met, CodeStringBuilder stbuilder)
        {

            //temp 
            List<MethodParameter> pars = met.pars;
            //call 
            stbuilder.AppendLine("//expand args");
            int j = pars.Count;
            if (j > 0)
            {
                //arg expansion   
                for (int i = 0; i < j; ++i)
                {
                    MethodParameter par = pars[i];
                    if (par.ArgByRef)
                    {
                        stbuilder.Append(par.InnerTypeName + " " + par.Name);
                        //with default value
                        if (par.InnerTypeName == "bool")
                        {
                            stbuilder.AppendLine("=false;");
                        }
                        else
                        {
                            stbuilder.AppendLine("=0;");
                        }
                    }
                }
            }
            //-------
            stbuilder.Append("i1.");//instant name
            stbuilder.Append(met.Name);
            stbuilder.Append("(");
            if (j > 0)
            {

                for (int i = 0; i < j; ++i)
                {
                    if (i > 0)
                    {
                        stbuilder.Append(",\r\n");
                    }
                    MethodParameter par = pars[i];
                    if (par.ArgByRef)
                    {
                        stbuilder.Append("ref " + par.Name);
                    }
                    else
                    {
                        stbuilder.Append("args." + par.Name + "()");
                    }
                }
            }
            stbuilder.AppendLine(");");
            if (j > 0)
            {
                for (int i = 0; i < j; ++i)
                {
                    MethodParameter par = pars[i];
                    if (par.ArgByRef)
                    {
                        stbuilder.AppendLine("args." + par.Name + "(" + par.Name + ");");
                    }
                }
            }
        }

        void GenerateCsSingleArgMethodImplForInterface(string argClassName, MethodPlan met, CodeStringBuilder stbuilder)
        {

            CodeMethodDeclaration metDecl = (CodeMethodDeclaration)met.metDecl;
            //temp 
            List<MethodParameter> pars = met.pars;

            stbuilder.Append("void");
            stbuilder.Append(" ");

            stbuilder.Append(met.Name);
            stbuilder.Append("(");
            stbuilder.Append(argClassName + " args");
            stbuilder.AppendLine(");");
        }
        void GenerateCsExpandedArgsMethodForInterface(MethodPlan met, CodeStringBuilder stbuilder)
        {
            CodeMethodDeclaration metDecl = (CodeMethodDeclaration)met.metDecl;

            //temp             
            stbuilder.Append(CefTypeTx.GetCsRetName(met.ReturnPlan.TypeSymbol));
            stbuilder.Append(" ");
            stbuilder.Append(met.Name);
            stbuilder.Append("(");

            List<CodeMethodParameter> pars = metDecl.Parameters;
            int j = pars.Count;
            for (int i = 0; i < j; ++i)
            {
                if (i > 0)
                {
                    stbuilder.Append(",");
                }

                MethodParameter parTx = met.pars[i];
                string parTypeName = CefTypeTx.GetCsRetName(parTx.TypeSymbol);
                stbuilder.Append(parTypeName);
                //some cpp name can't be use in C#
                stbuilder.Append(" ");
                stbuilder.Append(parTx.Name);
            }
            stbuilder.AppendLine(");");
        }

        string GenerateCsMethodArgsClass_Native(MethodPlan met, CodeStringBuilder stbuilder)
        {
            //generate cs method pars
            CodeMethodDeclaration metDecl = (CodeMethodDeclaration)met.metDecl;
            List<CodeMethodParameter> pars = metDecl.Parameters;
            int j = pars.Count;
            //temp 
            string className = met.Name + "NativeArgs";
            stbuilder.AppendLine("[StructLayout(LayoutKind.Sequential)]");
            stbuilder.AppendLine("struct " + className + "{ "); //this is private struct with explicit layout
            stbuilder.AppendLine("public int argFlags;");
            //
            for (int i = 0; i < j; ++i)
            {
                //move this to method
                CodeMethodParameter par = pars[i];
                MethodParameter parTx = met.pars[i];
                switch (parTx.Name)
                {
                    case "params":
                        parTx.Name = "_params";
                        break;
                    case "string":
                        parTx.Name = "_string";
                        break;
                    case "object":
                        parTx.Name = "_object";
                        break;
                    case "event":
                        parTx.Name = "_event";
                        break;
                }
                //
                stbuilder.Append("public ");

                string csParTypeName = CefTypeTx.GetCsRetName(parTx.TypeSymbol);
                string csSetterParTypeName = null;
                switch (csParTypeName)
                {
                    case "ref bool":
                        //provide both getter and setter method
                        //stbuilder.Append("bool");
                        parTx.ArgByRef = true;//temp
                        parTx.InnerTypeName = csSetterParTypeName = "bool";
                        break;
                    case "ref int":
                        //stbuilder.Append("int");
                        parTx.ArgByRef = true;//temp
                        parTx.InnerTypeName = csSetterParTypeName = "int";
                        break;
                    case "ref uint":
                        //stbuilder.Append("uint");
                        parTx.ArgByRef = true;//temp
                        parTx.InnerTypeName = csSetterParTypeName = "uint";
                        break;
                    default:
                        //stbuilder.Append(csParTypeName);
                        csSetterParTypeName = csParTypeName;
                        break;
                }

                //some cpp name can't be use in C#                 
                stbuilder.Append(" ");

                switch (csParTypeName)
                {
                    default:
                        {

                            if (csParTypeName.StartsWith("Cef"))
                            {
                                stbuilder.Append("IntPtr");
                            }
                            else if (csParTypeName.StartsWith("cef"))
                            {
                                stbuilder.Append(csParTypeName);
                            }
                            else
                            {
                                stbuilder.AppendLine(csParTypeName.ToString());
                                stbuilder.Append("IntPtr");
                            }
                        }
                        break;
                    case "IntPtr":
                        stbuilder.Append("IntPtr");
                        break;
                    case "List<object>":
                    case "List<string>":
                    case "List<CefCompositionUnderline>":
                        stbuilder.Append("IntPtr");
                        break;
                    case "CefValue":
                        stbuilder.Append("IntPtr");

                        break;
                    case "uint":
                        stbuilder.Append("uint");
                        break;
                    case "int":
                        stbuilder.Append("int");
                        break;
                    case "long":
                        stbuilder.Append("long");
                        break;
                    case "string":
                        stbuilder.Append("IntPtr");
                        break;
                    case "bool":
                        stbuilder.Append("bool");
                        break;
                    case "double":
                        stbuilder.Append("double");
                        break;
                    case "ref bool":
                        //provide both getter and setter method  
                        stbuilder.Append("double");
                        break;
                    case "ref int":
                        stbuilder.Append("int");
                        break;
                    case "ref uint":
                        stbuilder.Append("uint");
                        break;
                }
                stbuilder.Append(" ");
                stbuilder.Append(parTx.Name);
                stbuilder.AppendLine(";");
            }
            stbuilder.AppendLine("}"); //struct

            return className;
        }



        string GenerateCsMethodArgsClass_JsSlot(MethodPlan met, CodeStringBuilder stbuilder)
        {
            //generate cs method pars
            CodeMethodDeclaration metDecl = (CodeMethodDeclaration)met.metDecl;
            List<CodeMethodParameter> pars = metDecl.Parameters;
            int j = pars.Count;
            //temp 
            string className = met.Name + "Args";

            stbuilder.AppendLine("public struct " + className + "{ ");
            stbuilder.AppendLine("IntPtr nativePtr; //met arg native ptr");
            stbuilder.AppendLine("bool _isJsSlot;");

            stbuilder.AppendLine("internal " + className + "(IntPtr nativePtr){");

            stbuilder.AppendLine(@"int arg_flags;
                        this.nativePtr = MyMetArgs.GetNativeObjPtr(nativePtr,out arg_flags);
                        this._isJsSlot = ((arg_flags >> 18) & 1) ==0;
                        "
                        );

            stbuilder.AppendLine("}");

            int pos = 0;
            for (int i = 0; i < j; ++i)
            {
                pos = i + 1; //*** 

                //move this to method
                CodeMethodParameter par = pars[i];
                MethodParameter parTx = met.pars[i];
                switch (parTx.Name)
                {
                    case "params":
                        parTx.Name = "_params";
                        break;
                    case "string":
                        parTx.Name = "_string";
                        break;
                    case "object":
                        parTx.Name = "_object";
                        break;
                    case "event":
                        parTx.Name = "_event";
                        break;
                }
                //
                stbuilder.Append("public ");

                string csParTypeName = CefTypeTx.GetCsRetName(parTx.TypeSymbol);
                string csSetterParTypeName = null;
                switch (csParTypeName)
                {
                    case "ref bool":
                        //provide both getter and setter method
                        stbuilder.Append("bool");
                        parTx.ArgByRef = true;//temp
                        parTx.InnerTypeName = csSetterParTypeName = "bool";
                        break;
                    case "ref int":
                        stbuilder.Append("int");
                        parTx.ArgByRef = true;//temp
                        parTx.InnerTypeName = csSetterParTypeName = "int";
                        break;
                    case "ref uint":
                        stbuilder.Append("uint");
                        parTx.ArgByRef = true;//temp
                        parTx.InnerTypeName = csSetterParTypeName = "uint";
                        break;
                    default:
                        stbuilder.Append(csParTypeName);
                        csSetterParTypeName = csParTypeName;
                        break;
                }

                //some cpp name can't be use in C#                 
                stbuilder.Append(" ");
                stbuilder.Append(parTx.Name);
                stbuilder.Append("()");
                stbuilder.AppendLine("{");

                string nativeArgClassName = met.Name + "NativeArgs";

                switch (csParTypeName)
                {
                    default:
                        {
                            stbuilder.AppendLine("unsafe{"); //open unsafe
                            stbuilder.Append("return _isJsSlot ? \r\n");
                            //is-js-slot= true
                            if (csParTypeName.StartsWith("Cef"))
                            {
                                stbuilder.Append("new " + csParTypeName + "(MyMetArgs.GetAsIntPtr(nativePtr," + pos + "))");
                                //if is not js-slot (this is native arg )
                                stbuilder.Append(":\r\n");
                                //cast native ptr to specific c-struct and get specific o
                                stbuilder.Append("new " + csParTypeName + "(((" + nativeArgClassName + "*)this.nativePtr)->" + parTx.Name + ");"); ;
                            }
                            else if (csParTypeName.StartsWith("cef"))
                            {
                                stbuilder.Append("(" + csParTypeName + ")" + "MyMetArgs.GetAsInt32(nativePtr," + pos + ")");
                                //if is not js-slot (this is native arg )
                                stbuilder.Append(":\r\n");
                                //cast native ptr to specific c-struct and get specific o
                                stbuilder.Append("(" + csParTypeName + ")" + "(((" + nativeArgClassName + "*)this.nativePtr)->" + parTx.Name + ");");
                            }
                            else
                            {
                                stbuilder.Append("throw new CefNotImplementedException();");
                            }
                            stbuilder.AppendLine("}"); //close unsafe context
                        }
                        break;
                    case "IntPtr":
                        stbuilder.Append("throw new CefNotImplementedException();");
                        break;
                    case "List<object>":
                    case "List<string>":
                    case "List<CefCompositionUnderline>":
                        stbuilder.Append("throw new CefNotImplementedException();");
                        break;
                    case "CefValue":
                        stbuilder.Append("throw new CefNotImplementedException();");
                        break;
                    case "uint":
                        {
                            stbuilder.AppendLine("unsafe{");
                            stbuilder.Append(" return _isJsSlot ? \r\n" + "MyMetArgs.GetAsUInt32(nativePtr," + pos + ") :\r\n");
                            stbuilder.Append("((" + nativeArgClassName + "*)this.nativePtr)->" + parTx.Name);
                            stbuilder.AppendLine(";");
                            stbuilder.AppendLine("}"); //close unsafe context
                        }
                        break;
                    case "int":
                        {
                            stbuilder.AppendLine("unsafe{");
                            stbuilder.Append("return _isJsSlot? \r\n" + "MyMetArgs.GetAsInt32(nativePtr," + pos + "):\r\n");
                            stbuilder.Append("((" + nativeArgClassName + "*)this.nativePtr)->" + parTx.Name);
                            stbuilder.AppendLine(";");
                            stbuilder.AppendLine("}"); //close unsafe context
                        }
                        break;
                    case "long":
                        {
                            stbuilder.AppendLine("unsafe{");
                            stbuilder.Append("return _isJsSlot ? \r\n" + "MyMetArgs.GetAsInt64(nativePtr," + pos + "):\r\n");
                            stbuilder.Append("((" + nativeArgClassName + "*)this.nativePtr)->" + parTx.Name);
                            stbuilder.AppendLine(";");
                            stbuilder.AppendLine("}"); //close unsafe context 
                        }
                        break;
                    case "string":
                        {
                            stbuilder.AppendLine("unsafe{");
                            stbuilder.Append("return _isJsSlot ?\r\n" + "MyMetArgs.GetAsString(nativePtr," + pos + "):\r\n");
                            stbuilder.Append("MyMetArgs.GetAsString(((" + nativeArgClassName + "*)this.nativePtr)->" + parTx.Name + ")");
                            stbuilder.AppendLine(";");
                            stbuilder.AppendLine("}"); //close unsafe context  
                        }
                        break;
                    case "bool":
                        {
                            stbuilder.AppendLine("unsafe{");
                            stbuilder.Append("return _isJsSlot?\r\n" + "MyMetArgs.GetAsBool(nativePtr," + pos + "):\r\n");
                            stbuilder.Append("((" + nativeArgClassName + "*)this.nativePtr)->" + parTx.Name);
                            stbuilder.AppendLine(";");
                            stbuilder.AppendLine("}"); //close unsafe context  
                        }
                        break;
                    case "double":
                        {
                            stbuilder.AppendLine("unsafe{");
                            stbuilder.Append("return _isJsSlot?\r\n" + "MyMetArgs.GetAsDouble(nativePtr," + pos + "):\r\n");
                            stbuilder.Append("((" + nativeArgClassName + "*)this.nativePtr)->" + parTx.Name);
                            stbuilder.AppendLine(";");
                            stbuilder.AppendLine("}"); //close unsafe context  
                        }
                        break;
                    case "ref bool":
                        //provide both getter and setter method
                        {
                            stbuilder.AppendLine("unsafe{");
                            stbuilder.Append("return " + "MyMetArgs.GetAsBool(nativePtr," + pos + ");");
                            stbuilder.Append("}");//close unsafe context  
                            stbuilder.Append("}"); //close method

                            //----------------------------------------------------------------------------------
                            //method
                            //generate setter part

                            stbuilder.AppendLine("public void " + parTx.Name + "(" + csSetterParTypeName + " value){");
                            stbuilder.AppendLine("MyMetArgs.SetBoolToAddress(nativePtr," + pos + ",value);");
                            stbuilder.AppendLine("}");
                            continue;
                        }

                    case "ref int":
                        {
                            stbuilder.Append("return " + "MyMetArgs.GetAsInt32(nativePtr," + pos + ");");
                            stbuilder.AppendLine("}");

                            //method
                            //generate setter part
                            stbuilder.AppendLine("public void " + parTx.Name + "(" + csSetterParTypeName + " value){");
                            stbuilder.AppendLine("MyMetArgs.SetInt32ToAddress(nativePtr," + pos + ",value);");
                            stbuilder.AppendLine("}");
                            continue;
                        }

                    case "ref uint":
                        {
                            stbuilder.Append("return " + "MyMetArgs.GetAsUInt32(nativePtr," + pos + ");");
                            stbuilder.AppendLine("}");
                            //method
                            //generate setter part
                            stbuilder.AppendLine("public void " + parTx.Name + "(" + csSetterParTypeName + " value){");
                            stbuilder.AppendLine("MyMetArgs.SetUInt32ToAddress(nativePtr," + pos + ",value);");
                            stbuilder.AppendLine("}");
                            continue;
                        }

                }
                stbuilder.AppendLine("}"); //method
            }
            stbuilder.AppendLine("}"); //struct

            return className;
        }

    }
}