//---THIS-FILE-WAS-PATCHED , org=D:\projects\cef_binary_3.3071.1647.win32\cpptoc\resource_bundle_handler_cpptoc.cc
// Copyright (c) 2017 The Chromium Embedded Framework Authors. All rights
// reserved. Use of this source code is governed by a BSD-style license that
// can be found in the LICENSE file.
//
// ---------------------------------------------------------------------------
//
// This file was generated by the CEF translator tool. If making changes by
// hand only do so within the body of existing method and function
// implementations. See the translator.README.txt file in the tools directory
// for more information.
//
// $hash=47339f94c600cd510238e5994ac25684fe82fc14$
//

#include "libcef_dll/cpptoc/resource_bundle_handler_cpptoc.h"

//---kneadium-ext-begin
#include "../myext/ExportFuncAuto.h"
#include "../myext/InternalHeaderForExportFunc.h"
//---kneadium-ext-end

namespace {

// MEMBER FUNCTIONS - Body may be edited by hand.

int CEF_CALLBACK resource_bundle_handler_get_localized_string(
    struct _cef_resource_bundle_handler_t* self,
    int string_id,
    cef_string_t* string) {
  // AUTO-GENERATED CONTENT - DELETE THIS COMMENT BEFORE MODIFYING

  DCHECK(self);
  if (!self)
    return 0;
  // Verify param: string; type: string_byref
  DCHECK(string);
  if (!string)
    return 0;

  // Translate param: string; type: string_byref
  CefString stringStr(string);

//---kneadium-ext-begin3
#if ENABLE_KNEADIUM_EXT
auto me = CefResourceBundleHandlerCppToC::Get(self);
const int CALLER_CODE=(CefResourceBundleHandlerExt::_typeName << 16) | CefResourceBundleHandlerExt::CefResourceBundleHandlerExt_GetLocalizedString_1;
auto m_callback= me->GetManagedCallBack(CALLER_CODE);
if(m_callback){
CefString tmp_arg2 (string);
CefResourceBundleHandlerExt::GetLocalizedStringArgs args1(string_id,&tmp_arg2);
m_callback(CALLER_CODE, &args1.arg);
 if (((args1.arg.myext_flags >> 21) & 1) == 1){
 return args1.arg.myext_ret_value;
}
}
#endif
//---kneadium-ext-end

  // Execute
  bool _retval = CefResourceBundleHandlerCppToC::Get(self)->GetLocalizedString(
      string_id, stringStr);

  // Return type: bool
  return _retval;
}

int CEF_CALLBACK resource_bundle_handler_get_data_resource(
    struct _cef_resource_bundle_handler_t* self,
    int resource_id,
    void** data,
    size_t* data_size) {
  // AUTO-GENERATED CONTENT - DELETE THIS COMMENT BEFORE MODIFYING

  DCHECK(self);
  if (!self)
    return 0;
  // Verify param: data; type: simple_byref
  DCHECK(data);
  if (!data)
    return 0;
  // Verify param: data_size; type: simple_byref
  DCHECK(data_size);
  if (!data_size)
    return 0;

  // Translate param: data; type: simple_byref
  void* dataVal = data ? *data : NULL;
  // Translate param: data_size; type: simple_byref
  size_t data_sizeVal = data_size ? *data_size : 0;

//---kneadium-ext-begin2
#if ENABLE_KNEADIUM_EXT
auto me = CefResourceBundleHandlerCppToC::Get(self);
const int CALLER_CODE=(CefResourceBundleHandlerExt::_typeName << 16) | CefResourceBundleHandlerExt::CefResourceBundleHandlerExt_GetDataResource_2;
auto m_callback= me->GetManagedCallBack(CALLER_CODE);
if(m_callback){
CefResourceBundleHandlerExt::GetDataResourceArgs args1(resource_id,&dataVal,&data_sizeVal);
m_callback(CALLER_CODE, &args1.arg);
 if (((args1.arg.myext_flags >> 21) & 1) == 1){
// Restore param: data; type: simple_byref
if (data)
*data = dataVal;

// Restore param: data_size; type: simple_byref
if (data_size)
*data_size = data_sizeVal;


 return args1.arg.myext_ret_value;
}
}
#endif
//---kneadium-ext-end

  // Execute
  bool _retval = CefResourceBundleHandlerCppToC::Get(self)->GetDataResource(
      resource_id, dataVal, data_sizeVal);

  // Restore param: data; type: simple_byref
  if (data)
    *data = dataVal;
  // Restore param: data_size; type: simple_byref
  if (data_size)
    *data_size = data_sizeVal;

  // Return type: bool
  return _retval;
}

int CEF_CALLBACK resource_bundle_handler_get_data_resource_for_scale(
    struct _cef_resource_bundle_handler_t* self,
    int resource_id,
    cef_scale_factor_t scale_factor,
    void** data,
    size_t* data_size) {
  // AUTO-GENERATED CONTENT - DELETE THIS COMMENT BEFORE MODIFYING

  DCHECK(self);
  if (!self)
    return 0;
  // Verify param: data; type: simple_byref
  DCHECK(data);
  if (!data)
    return 0;
  // Verify param: data_size; type: simple_byref
  DCHECK(data_size);
  if (!data_size)
    return 0;

  // Translate param: data; type: simple_byref
  void* dataVal = data ? *data : NULL;
  // Translate param: data_size; type: simple_byref
  size_t data_sizeVal = data_size ? *data_size : 0;

//---kneadium-ext-begin1
#if ENABLE_KNEADIUM_EXT
auto me = CefResourceBundleHandlerCppToC::Get(self);
const int CALLER_CODE=(CefResourceBundleHandlerExt::_typeName << 16) | CefResourceBundleHandlerExt::CefResourceBundleHandlerExt_GetDataResourceForScale_3;
auto m_callback= me->GetManagedCallBack(CALLER_CODE);
if(m_callback){
CefResourceBundleHandlerExt::GetDataResourceForScaleArgs args1(resource_id,scale_factor,&dataVal,&data_sizeVal);
m_callback(CALLER_CODE, &args1.arg);
 if (((args1.arg.myext_flags >> 21) & 1) == 1){
// Restore param: data; type: simple_byref
if (data)
*data = dataVal;

// Restore param: data_size; type: simple_byref
if (data_size)
*data_size = data_sizeVal;


 return args1.arg.myext_ret_value;
}
}
#endif
//---kneadium-ext-end

  // Execute
  bool _retval =
      CefResourceBundleHandlerCppToC::Get(self)->GetDataResourceForScale(
          resource_id, scale_factor, dataVal, data_sizeVal);

  // Restore param: data; type: simple_byref
  if (data)
    *data = dataVal;
  // Restore param: data_size; type: simple_byref
  if (data_size)
    *data_size = data_sizeVal;

  // Return type: bool
  return _retval;
}

}  // namespace

// CONSTRUCTOR - Do not edit by hand.

CefResourceBundleHandlerCppToC::CefResourceBundleHandlerCppToC() {
  GetStruct()->get_localized_string =
      resource_bundle_handler_get_localized_string;
  GetStruct()->get_data_resource = resource_bundle_handler_get_data_resource;
  GetStruct()->get_data_resource_for_scale =
      resource_bundle_handler_get_data_resource_for_scale;
}

template <>
CefRefPtr<CefResourceBundleHandler> CefCppToCRefCounted<
    CefResourceBundleHandlerCppToC,
    CefResourceBundleHandler,
    cef_resource_bundle_handler_t>::UnwrapDerived(CefWrapperType type,
                                                  cef_resource_bundle_handler_t*
                                                      s) {
  NOTREACHED() << "Unexpected class type: " << type;
  return NULL;
}

#if DCHECK_IS_ON()
template <>
base::AtomicRefCount
    CefCppToCRefCounted<CefResourceBundleHandlerCppToC,
                        CefResourceBundleHandler,
                        cef_resource_bundle_handler_t>::DebugObjCt = 0;
#endif

template <>
CefWrapperType
    CefCppToCRefCounted<CefResourceBundleHandlerCppToC,
                        CefResourceBundleHandler,
                        cef_resource_bundle_handler_t>::kWrapperType =
        WT_RESOURCE_BUNDLE_HANDLER;
