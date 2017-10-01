//---THIS-FILE-WAS-PATCHED , org=D:\projects\cef_binary_3.3071.1647.win32\cpptoc\geolocation_handler_cpptoc.cc
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
// $hash=8e37dbdeb9b4552a0c19c34e0ad7855a1c5ef709$
//

#include "libcef_dll/cpptoc/geolocation_handler_cpptoc.h"
#include "libcef_dll/ctocpp/browser_ctocpp.h"
#include "libcef_dll/ctocpp/geolocation_callback_ctocpp.h"

//---kneadium-ext-begin
#include "../myext/ExportFuncAuto.h"
#include "../myext/InternalHeaderForExportFunc.h"
//---kneadium-ext-end

namespace {

// MEMBER FUNCTIONS - Body may be edited by hand.

int CEF_CALLBACK geolocation_handler_on_request_geolocation_permission(
    struct _cef_geolocation_handler_t* self,
    cef_browser_t* browser,
    const cef_string_t* requesting_url,
    int request_id,
    cef_geolocation_callback_t* callback) {
  // AUTO-GENERATED CONTENT - DELETE THIS COMMENT BEFORE MODIFYING

  DCHECK(self);
  if (!self)
    return 0;
  // Verify param: browser; type: refptr_diff
  DCHECK(browser);
  if (!browser)
    return 0;
  // Verify param: requesting_url; type: string_byref_const
  DCHECK(requesting_url);
  if (!requesting_url)
    return 0;
  // Verify param: callback; type: refptr_diff
  DCHECK(callback);
  if (!callback)
    return 0;

//---kneadium-ext-begin2
#if ENABLE_KNEADIUM_EXT
auto me = CefGeolocationHandlerCppToC::Get(self);
const int CALLER_CODE=(CefGeolocationHandlerExt::_typeName << 16) | CefGeolocationHandlerExt::CefGeolocationHandlerExt_OnRequestGeolocationPermission_1;
auto m_callback= me->GetManagedCallBack(CALLER_CODE);
if(m_callback){
CefString tmp_arg2 (requesting_url);
CefGeolocationHandlerExt::OnRequestGeolocationPermissionArgs args1(browser,tmp_arg2,request_id,callback);
m_callback(CALLER_CODE, &args1.arg);
 if (((args1.arg.myext_flags >> 21) & 1) == 1){
 return args1.arg.myext_ret_value;
}
}
#endif
//---kneadium-ext-end

  // Execute
  bool _retval =
      CefGeolocationHandlerCppToC::Get(self)->OnRequestGeolocationPermission(
          CefBrowserCToCpp::Wrap(browser), CefString(requesting_url),
          request_id, CefGeolocationCallbackCToCpp::Wrap(callback));

  // Return type: bool
  return _retval;
}

void CEF_CALLBACK geolocation_handler_on_cancel_geolocation_permission(
    struct _cef_geolocation_handler_t* self,
    cef_browser_t* browser,
    int request_id) {
  // AUTO-GENERATED CONTENT - DELETE THIS COMMENT BEFORE MODIFYING

  DCHECK(self);
  if (!self)
    return;
  // Verify param: browser; type: refptr_diff
  DCHECK(browser);
  if (!browser)
    return;

//---kneadium-ext-begin1
#if ENABLE_KNEADIUM_EXT
auto me = CefGeolocationHandlerCppToC::Get(self);
const int CALLER_CODE=(CefGeolocationHandlerExt::_typeName << 16) | CefGeolocationHandlerExt::CefGeolocationHandlerExt_OnCancelGeolocationPermission_2;
auto m_callback= me->GetManagedCallBack(CALLER_CODE);
if(m_callback){
CefGeolocationHandlerExt::OnCancelGeolocationPermissionArgs args1(browser,request_id);
m_callback(CALLER_CODE, &args1.arg);
 if (((args1.arg.myext_flags >> 21) & 1) == 1){
return;
}
}
#endif
//---kneadium-ext-end

  // Execute
  CefGeolocationHandlerCppToC::Get(self)->OnCancelGeolocationPermission(
      CefBrowserCToCpp::Wrap(browser), request_id);
}

}  // namespace

// CONSTRUCTOR - Do not edit by hand.

CefGeolocationHandlerCppToC::CefGeolocationHandlerCppToC() {
  GetStruct()->on_request_geolocation_permission =
      geolocation_handler_on_request_geolocation_permission;
  GetStruct()->on_cancel_geolocation_permission =
      geolocation_handler_on_cancel_geolocation_permission;
}

template <>
CefRefPtr<CefGeolocationHandler> CefCppToCRefCounted<
    CefGeolocationHandlerCppToC,
    CefGeolocationHandler,
    cef_geolocation_handler_t>::UnwrapDerived(CefWrapperType type,
                                              cef_geolocation_handler_t* s) {
  NOTREACHED() << "Unexpected class type: " << type;
  return NULL;
}

#if DCHECK_IS_ON()
template <>
base::AtomicRefCount
    CefCppToCRefCounted<CefGeolocationHandlerCppToC,
                        CefGeolocationHandler,
                        cef_geolocation_handler_t>::DebugObjCt = 0;
#endif

template <>
CefWrapperType CefCppToCRefCounted<CefGeolocationHandlerCppToC,
                                   CefGeolocationHandler,
                                   cef_geolocation_handler_t>::kWrapperType =
    WT_GEOLOCATION_HANDLER;
