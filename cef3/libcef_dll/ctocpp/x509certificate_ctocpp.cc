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
// $hash=e6992d1ee8d86f4289cb51e155bce1990879e4db$
//

#include "libcef_dll/ctocpp/x509certificate_ctocpp.h"
#include <algorithm>
#include "libcef_dll/ctocpp/binary_value_ctocpp.h"
#include "libcef_dll/ctocpp/x509cert_principal_ctocpp.h"

// VIRTUAL METHODS - Body may be edited by hand.

CefRefPtr<CefX509CertPrincipal> CefX509CertificateCToCpp::GetSubject() {
  cef_x509certificate_t* _struct = GetStruct();
  if (CEF_MEMBER_MISSING(_struct, get_subject))
    return NULL;

  // AUTO-GENERATED CONTENT - DELETE THIS COMMENT BEFORE MODIFYING

  // Execute
  cef_x509cert_principal_t* _retval = _struct->get_subject(_struct);

  // Return type: refptr_same
  return CefX509CertPrincipalCToCpp::Wrap(_retval);
}

CefRefPtr<CefX509CertPrincipal> CefX509CertificateCToCpp::GetIssuer() {
  cef_x509certificate_t* _struct = GetStruct();
  if (CEF_MEMBER_MISSING(_struct, get_issuer))
    return NULL;

  // AUTO-GENERATED CONTENT - DELETE THIS COMMENT BEFORE MODIFYING

  // Execute
  cef_x509cert_principal_t* _retval = _struct->get_issuer(_struct);

  // Return type: refptr_same
  return CefX509CertPrincipalCToCpp::Wrap(_retval);
}

CefRefPtr<CefBinaryValue> CefX509CertificateCToCpp::GetSerialNumber() {
  cef_x509certificate_t* _struct = GetStruct();
  if (CEF_MEMBER_MISSING(_struct, get_serial_number))
    return NULL;

  // AUTO-GENERATED CONTENT - DELETE THIS COMMENT BEFORE MODIFYING

  // Execute
  cef_binary_value_t* _retval = _struct->get_serial_number(_struct);

  // Return type: refptr_same
  return CefBinaryValueCToCpp::Wrap(_retval);
}

CefTime CefX509CertificateCToCpp::GetValidStart() {
  cef_x509certificate_t* _struct = GetStruct();
  if (CEF_MEMBER_MISSING(_struct, get_valid_start))
    return CefTime();

  // AUTO-GENERATED CONTENT - DELETE THIS COMMENT BEFORE MODIFYING

  // Execute
  cef_time_t _retval = _struct->get_valid_start(_struct);

  // Return type: simple
  return _retval;
}

CefTime CefX509CertificateCToCpp::GetValidExpiry() {
  cef_x509certificate_t* _struct = GetStruct();
  if (CEF_MEMBER_MISSING(_struct, get_valid_expiry))
    return CefTime();

  // AUTO-GENERATED CONTENT - DELETE THIS COMMENT BEFORE MODIFYING

  // Execute
  cef_time_t _retval = _struct->get_valid_expiry(_struct);

  // Return type: simple
  return _retval;
}

CefRefPtr<CefBinaryValue> CefX509CertificateCToCpp::GetDEREncoded() {
  cef_x509certificate_t* _struct = GetStruct();
  if (CEF_MEMBER_MISSING(_struct, get_derencoded))
    return NULL;

  // AUTO-GENERATED CONTENT - DELETE THIS COMMENT BEFORE MODIFYING

  // Execute
  cef_binary_value_t* _retval = _struct->get_derencoded(_struct);

  // Return type: refptr_same
  return CefBinaryValueCToCpp::Wrap(_retval);
}

CefRefPtr<CefBinaryValue> CefX509CertificateCToCpp::GetPEMEncoded() {
  cef_x509certificate_t* _struct = GetStruct();
  if (CEF_MEMBER_MISSING(_struct, get_pemencoded))
    return NULL;

  // AUTO-GENERATED CONTENT - DELETE THIS COMMENT BEFORE MODIFYING

  // Execute
  cef_binary_value_t* _retval = _struct->get_pemencoded(_struct);

  // Return type: refptr_same
  return CefBinaryValueCToCpp::Wrap(_retval);
}

size_t CefX509CertificateCToCpp::GetIssuerChainSize() {
  cef_x509certificate_t* _struct = GetStruct();
  if (CEF_MEMBER_MISSING(_struct, get_issuer_chain_size))
    return 0;

  // AUTO-GENERATED CONTENT - DELETE THIS COMMENT BEFORE MODIFYING

  // Execute
  size_t _retval = _struct->get_issuer_chain_size(_struct);

  // Return type: simple
  return _retval;
}

void CefX509CertificateCToCpp::GetDEREncodedIssuerChain(
    IssuerChainBinaryList& chain) {
  cef_x509certificate_t* _struct = GetStruct();
  if (CEF_MEMBER_MISSING(_struct, get_derencoded_issuer_chain))
    return;

  // AUTO-GENERATED CONTENT - DELETE THIS COMMENT BEFORE MODIFYING

  // Translate param: chain; type: refptr_vec_same_byref
  size_t chainSize = chain.size();
  size_t chainCount = std::max(GetIssuerChainSize(), chainSize);
  cef_binary_value_t** chainList = NULL;
  if (chainCount > 0) {
    chainList = new cef_binary_value_t*[chainCount];
    DCHECK(chainList);
    if (chainList) {
      memset(chainList, 0, sizeof(cef_binary_value_t*) * chainCount);
    }
    if (chainList && chainSize > 0) {
      for (size_t i = 0; i < chainSize; ++i) {
        chainList[i] = CefBinaryValueCToCpp::Unwrap(chain[i]);
      }
    }
  }

  // Execute
  _struct->get_derencoded_issuer_chain(_struct, &chainCount, chainList);

  // Restore param:chain; type: refptr_vec_same_byref
  chain.clear();
  if (chainCount > 0 && chainList) {
    for (size_t i = 0; i < chainCount; ++i) {
      chain.push_back(CefBinaryValueCToCpp::Wrap(chainList[i]));
    }
    delete[] chainList;
  }
}

void CefX509CertificateCToCpp::GetPEMEncodedIssuerChain(
    IssuerChainBinaryList& chain) {
  cef_x509certificate_t* _struct = GetStruct();
  if (CEF_MEMBER_MISSING(_struct, get_pemencoded_issuer_chain))
    return;

  // AUTO-GENERATED CONTENT - DELETE THIS COMMENT BEFORE MODIFYING

  // Translate param: chain; type: refptr_vec_same_byref
  size_t chainSize = chain.size();
  size_t chainCount = std::max(GetIssuerChainSize(), chainSize);
  cef_binary_value_t** chainList = NULL;
  if (chainCount > 0) {
    chainList = new cef_binary_value_t*[chainCount];
    DCHECK(chainList);
    if (chainList) {
      memset(chainList, 0, sizeof(cef_binary_value_t*) * chainCount);
    }
    if (chainList && chainSize > 0) {
      for (size_t i = 0; i < chainSize; ++i) {
        chainList[i] = CefBinaryValueCToCpp::Unwrap(chain[i]);
      }
    }
  }

  // Execute
  _struct->get_pemencoded_issuer_chain(_struct, &chainCount, chainList);

  // Restore param:chain; type: refptr_vec_same_byref
  chain.clear();
  if (chainCount > 0 && chainList) {
    for (size_t i = 0; i < chainCount; ++i) {
      chain.push_back(CefBinaryValueCToCpp::Wrap(chainList[i]));
    }
    delete[] chainList;
  }
}

// CONSTRUCTOR - Do not edit by hand.

CefX509CertificateCToCpp::CefX509CertificateCToCpp() {}

template <>
cef_x509certificate_t* CefCToCppRefCounted<
    CefX509CertificateCToCpp,
    CefX509Certificate,
    cef_x509certificate_t>::UnwrapDerived(CefWrapperType type,
                                          CefX509Certificate* c) {
  NOTREACHED() << "Unexpected class type: " << type;
  return NULL;
}

#if DCHECK_IS_ON()
template <>
base::AtomicRefCount CefCToCppRefCounted<CefX509CertificateCToCpp,
                                         CefX509Certificate,
                                         cef_x509certificate_t>::DebugObjCt = 0;
#endif

template <>
CefWrapperType CefCToCppRefCounted<CefX509CertificateCToCpp,
                                   CefX509Certificate,
                                   cef_x509certificate_t>::kWrapperType =
    WT_X509CERTIFICATE;
