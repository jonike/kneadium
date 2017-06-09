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
// $hash=190878db3c35559a52669c437c3e4392fddfd07f$
//

#include "libcef_dll/ctocpp/v8stack_frame_ctocpp.h"

// VIRTUAL METHODS - Body may be edited by hand.

bool CefV8StackFrameCToCpp::IsValid() {
  cef_v8stack_frame_t* _struct = GetStruct();
  if (CEF_MEMBER_MISSING(_struct, is_valid))
    return false;

  // AUTO-GENERATED CONTENT - DELETE THIS COMMENT BEFORE MODIFYING

  // Execute
  int _retval = _struct->is_valid(_struct);

  // Return type: bool
  return _retval ? true : false;
}

CefString CefV8StackFrameCToCpp::GetScriptName() {
  cef_v8stack_frame_t* _struct = GetStruct();
  if (CEF_MEMBER_MISSING(_struct, get_script_name))
    return CefString();

  // AUTO-GENERATED CONTENT - DELETE THIS COMMENT BEFORE MODIFYING

  // Execute
  cef_string_userfree_t _retval = _struct->get_script_name(_struct);

  // Return type: string
  CefString _retvalStr;
  _retvalStr.AttachToUserFree(_retval);
  return _retvalStr;
}

CefString CefV8StackFrameCToCpp::GetScriptNameOrSourceURL() {
  cef_v8stack_frame_t* _struct = GetStruct();
  if (CEF_MEMBER_MISSING(_struct, get_script_name_or_source_url))
    return CefString();

  // AUTO-GENERATED CONTENT - DELETE THIS COMMENT BEFORE MODIFYING

  // Execute
  cef_string_userfree_t _retval =
      _struct->get_script_name_or_source_url(_struct);

  // Return type: string
  CefString _retvalStr;
  _retvalStr.AttachToUserFree(_retval);
  return _retvalStr;
}

CefString CefV8StackFrameCToCpp::GetFunctionName() {
  cef_v8stack_frame_t* _struct = GetStruct();
  if (CEF_MEMBER_MISSING(_struct, get_function_name))
    return CefString();

  // AUTO-GENERATED CONTENT - DELETE THIS COMMENT BEFORE MODIFYING

  // Execute
  cef_string_userfree_t _retval = _struct->get_function_name(_struct);

  // Return type: string
  CefString _retvalStr;
  _retvalStr.AttachToUserFree(_retval);
  return _retvalStr;
}

int CefV8StackFrameCToCpp::GetLineNumber() {
  cef_v8stack_frame_t* _struct = GetStruct();
  if (CEF_MEMBER_MISSING(_struct, get_line_number))
    return 0;

  // AUTO-GENERATED CONTENT - DELETE THIS COMMENT BEFORE MODIFYING

  // Execute
  int _retval = _struct->get_line_number(_struct);

  // Return type: simple
  return _retval;
}

int CefV8StackFrameCToCpp::GetColumn() {
  cef_v8stack_frame_t* _struct = GetStruct();
  if (CEF_MEMBER_MISSING(_struct, get_column))
    return 0;

  // AUTO-GENERATED CONTENT - DELETE THIS COMMENT BEFORE MODIFYING

  // Execute
  int _retval = _struct->get_column(_struct);

  // Return type: simple
  return _retval;
}

bool CefV8StackFrameCToCpp::IsEval() {
  cef_v8stack_frame_t* _struct = GetStruct();
  if (CEF_MEMBER_MISSING(_struct, is_eval))
    return false;

  // AUTO-GENERATED CONTENT - DELETE THIS COMMENT BEFORE MODIFYING

  // Execute
  int _retval = _struct->is_eval(_struct);

  // Return type: bool
  return _retval ? true : false;
}

bool CefV8StackFrameCToCpp::IsConstructor() {
  cef_v8stack_frame_t* _struct = GetStruct();
  if (CEF_MEMBER_MISSING(_struct, is_constructor))
    return false;

  // AUTO-GENERATED CONTENT - DELETE THIS COMMENT BEFORE MODIFYING

  // Execute
  int _retval = _struct->is_constructor(_struct);

  // Return type: bool
  return _retval ? true : false;
}

// CONSTRUCTOR - Do not edit by hand.

CefV8StackFrameCToCpp::CefV8StackFrameCToCpp() {}

template <>
cef_v8stack_frame_t*
CefCToCppRefCounted<CefV8StackFrameCToCpp,
                    CefV8StackFrame,
                    cef_v8stack_frame_t>::UnwrapDerived(CefWrapperType type,
                                                        CefV8StackFrame* c) {
  NOTREACHED() << "Unexpected class type: " << type;
  return NULL;
}

#if DCHECK_IS_ON()
template <>
base::AtomicRefCount CefCToCppRefCounted<CefV8StackFrameCToCpp,
                                         CefV8StackFrame,
                                         cef_v8stack_frame_t>::DebugObjCt = 0;
#endif

template <>
CefWrapperType CefCToCppRefCounted<CefV8StackFrameCToCpp,
                                   CefV8StackFrame,
                                   cef_v8stack_frame_t>::kWrapperType =
    WT_V8STACK_FRAME;
