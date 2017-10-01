//---THIS-FILE-WAS-PATCHED , org=D:\projects\cef_binary_3.3071.1647.win32\cpptoc\dialog_handler_cpptoc.cc
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
// $hash=2905826a83c8afbda57e2a6868bb447f8f22b58b$
//

#include "libcef_dll/cpptoc/dialog_handler_cpptoc.h"
#include "libcef_dll/ctocpp/browser_ctocpp.h"
#include "libcef_dll/ctocpp/file_dialog_callback_ctocpp.h"
#include "libcef_dll/transfer_util.h"

//---kneadium-ext-begin
#include "../myext/ExportFuncAuto.h"
#include "../myext/InternalHeaderForExportFunc.h"
//---kneadium-ext-end

namespace {

// MEMBER FUNCTIONS - Body may be edited by hand.

int CEF_CALLBACK
dialog_handler_on_file_dialog(struct _cef_dialog_handler_t* self,
                              cef_browser_t* browser,
                              cef_file_dialog_mode_t mode,
                              const cef_string_t* title,
                              const cef_string_t* default_file_path,
                              cef_string_list_t accept_filters,
                              int selected_accept_filter,
                              cef_file_dialog_callback_t* callback) {
  // AUTO-GENERATED CONTENT - DELETE THIS COMMENT BEFORE MODIFYING

  DCHECK(self);
  if (!self)
    return 0;
  // Verify param: browser; type: refptr_diff
  DCHECK(browser);
  if (!browser)
    return 0;
  // Verify param: selected_accept_filter; type: simple_byval
  DCHECK_GE(selected_accept_filter, 0);
  if (selected_accept_filter < 0)
    return 0;
  // Verify param: callback; type: refptr_diff
  DCHECK(callback);
  if (!callback)
    return 0;
  // Unverified params: title, default_file_path, accept_filters

  // Translate param: accept_filters; type: string_vec_byref_const
  std::vector<CefString> accept_filtersList;
  transfer_string_list_contents(accept_filters, accept_filtersList);

//---kneadium-ext-begin1
#if ENABLE_KNEADIUM_EXT
auto me = CefDialogHandlerCppToC::Get(self);
const int CALLER_CODE=(CefDialogHandlerExt::_typeName << 16) | CefDialogHandlerExt::CefDialogHandlerExt_OnFileDialog_1;
auto m_callback= me->GetManagedCallBack(CALLER_CODE);
if(m_callback){
CefString tmp_arg3 (title);
CefString tmp_arg4 (default_file_path);
CefDialogHandlerExt::OnFileDialogArgs args1(browser,mode,tmp_arg3,tmp_arg4,&accept_filtersList,selected_accept_filter,callback);
m_callback(CALLER_CODE, &args1.arg);
 if (((args1.arg.myext_flags >> 21) & 1) == 1){
 return args1.arg.myext_ret_value;
}
}
#endif
//---kneadium-ext-end

  // Execute
  bool _retval = CefDialogHandlerCppToC::Get(self)->OnFileDialog(
      CefBrowserCToCpp::Wrap(browser), mode, CefString(title),
      CefString(default_file_path), accept_filtersList, selected_accept_filter,
      CefFileDialogCallbackCToCpp::Wrap(callback));

  // Return type: bool
  return _retval;
}

}  // namespace

// CONSTRUCTOR - Do not edit by hand.

CefDialogHandlerCppToC::CefDialogHandlerCppToC() {
  GetStruct()->on_file_dialog = dialog_handler_on_file_dialog;
}

template <>
CefRefPtr<CefDialogHandler> CefCppToCRefCounted<
    CefDialogHandlerCppToC,
    CefDialogHandler,
    cef_dialog_handler_t>::UnwrapDerived(CefWrapperType type,
                                         cef_dialog_handler_t* s) {
  NOTREACHED() << "Unexpected class type: " << type;
  return NULL;
}

#if DCHECK_IS_ON()
template <>
base::AtomicRefCount CefCppToCRefCounted<CefDialogHandlerCppToC,
                                         CefDialogHandler,
                                         cef_dialog_handler_t>::DebugObjCt = 0;
#endif

template <>
CefWrapperType CefCppToCRefCounted<CefDialogHandlerCppToC,
                                   CefDialogHandler,
                                   cef_dialog_handler_t>::kWrapperType =
    WT_DIALOG_HANDLER;
