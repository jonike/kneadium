//###_ORIGINAL D:\projects\cef_binary_3.3071.1647.win64\tests\cefclient\browser//window_test_runner_views.h
// Copyright (c) 2016 The Chromium Embedded Framework Authors. All rights
// reserved. Use of this source code is governed by a BSD-style license that
// can be found in the LICENSE file.

#ifndef CEF_TESTS_CEFCLIENT_BROWSER_WINDOW_TEST_RUNNER_VIEWS_H_
#define CEF_TESTS_CEFCLIENT_BROWSER_WINDOW_TEST_RUNNER_VIEWS_H_
#pragma once

#include "tests/cefclient/browser/window_test_runner.h"

namespace client {
namespace window_test {

//###_BEGIN
#include "tests/cefclient/myext/mycef_buildconfig.h"
#if BUILD_TEST
//###_END

// Views platform implementation.
class WindowTestRunnerViews : public WindowTestRunner {
 public:
  WindowTestRunnerViews();

  void SetPos(CefRefPtr<CefBrowser> browser,
              int x,
              int y,
              int width,
              int height) OVERRIDE;
  void Minimize(CefRefPtr<CefBrowser> browser) OVERRIDE;
  void Maximize(CefRefPtr<CefBrowser> browser) OVERRIDE;
  void Restore(CefRefPtr<CefBrowser> browser) OVERRIDE;
};

//###_BEGIN
#endif // BUILD_TEST
//###_END

}  // namespace window_test
}  // namespace client

#endif  // CEF_TESTS_CEFCLIENT_BROWSER_WINDOW_TEST_RUNNER_VIEWS_H_
