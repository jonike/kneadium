﻿using System;
using Pencil.Gaming;
using LayoutFarm.CefBridge;
using PixelFarm.Forms;

namespace TestGlfw
{

    class SimpleWindowProgram
    {
        static void CheckNativeLibs(string[] args)
        {
            //where are native lib/exe. 
            //set proper dir here
            //depend on what you want
            //1. nearest local dir
            //2. common dir  
            //string currrentExecPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            //string commonAppDir = System.IO.Path.GetDirectoryName(Application.CommonAppDataPath);//skip version
            //------  
            ReferencePaths.LIB_PATH = @"D:\projects/cef_3_3071.1647/win64";//*** 64 bits
            ReferencePaths.SUB_PROCESS_PATH = ReferencePaths.LIB_PATH + "/CefBwSp.exe";
            //---------------
            ReferencePaths.OUTPUT_DIR = @"../../../_output";//dir
            ReferencePaths.LOG_PATH = ReferencePaths.OUTPUT_DIR + "/cef_console.log"; //file
            ReferencePaths.CACHE_PATH = ReferencePaths.OUTPUT_DIR + "/cef_cache"; //dir
            ReferencePaths.SAVE_IMAGE_PATH = ReferencePaths.OUTPUT_DIR + "/snap02"; //dir


            //reset
            ReferencePaths.LIB_PATH = @"D:\projects/cef_3_3071.1647/win64";//*** 64 bits
            ReferencePaths.SUB_PROCESS_PATH = ReferencePaths.LIB_PATH + "/CefBwSp.exe";


            //----  
            CreateFolderIfNotExist(ReferencePaths.OUTPUT_DIR);
            CreateFolderIfNotExist(ReferencePaths.CACHE_PATH);
            CreateFolderIfNotExist(ReferencePaths.SAVE_IMAGE_PATH);
        }
        static void CreateFolderIfNotExist(string folderName)
        {
            if (!System.IO.Directory.Exists(folderName))
            {
                System.IO.Directory.CreateDirectory(folderName);
            }
        }

        public static void Start(string[] args)
        {
            if (!Glfw.Init())
            {
                Console.WriteLine("can't init glfw");
                return;
            }
            //this is designed for cef UI process.
            //this process starts before any subprocess.
            //so before load anything we should check  
            //  if essential libs are available
            //------------------------------------------
            CheckNativeLibs(args);
            MyCef3InitEssential.SkipPreRun(true);
            //------------------------------------------
            //1. load cef before OLE init (eg init winform) ***
            //see more detail ...  MyCef3InitEssential
            if (!MyCef3InitEssential.LoadAndInitCef3(args))
            {
                return;
            }
            //------------------------------------------ 
            //1. if this is main UI process
            //the code go here, and we just start
            //winform app as usual
            //2. if this is other process
            //mean this process is finish and will terminate soon.
            //so we do noting, just exit!
            //(***please note that 
            //*** we call ShutDownCef3 only in main thread ***)

            if (!MyCef3InitEssential.IsInMainProcess)
            {
                MyCef3InitEssential.ClearRemainingCefMsg();
                return;
            }

            //1. load cef before OLE init (eg init winform) ***
            //see more detail ...  MyCef3InitEssential
            if (!MyCef3InitEssential.LoadAndInitCef3(args))
            {
                return;
            }

            //------------------------------------------
            /////////////////////////////////////////////
            //this code is run only in main process
            //------------------------------------------            
            GlFwForm glfwForm = GlfwApp.CreateGlfwForm(
                800, 600,
                "Native CefBrowser, Press any key to start browse the web");
            ////--------------- 


            MyFormWrapper formMain = new MyFormWrapper(glfwForm.Handle);

            //add cef-browser into glfw form
            cefBrowser = new MyCefBrowser(
               formMain,
               0, 0, 800, 600,
               "about:blank", false);

            //
            //Form f1 = Form.CreateFromNativeWindowHwnd2(glfwForm.Handle);
            //f1.Width = glfwForm.Width;
            //f1.Height = glfwForm.Height;

            //Glfw.SetWindowSizeCallback(glWindow, (GlfwWindowPtr wnd, int width, int height) =>
            //{
            //    //change window size here
            //});
            //AddWbControlToMainWindow(f1);

            bool isCreated = false;
            while (!GlfwApp.ShouldClose())
            {

                if (!isCreated)
                {
                    if (cefBrowser.IsBrowserCreated)
                    {
                        cefBrowser.NavigateTo("https://google.com");
                        isCreated = true;
                    }
                }
                MyCef3InitEssential.CefDoMessageLoopWork();
                Glfw.PollEvents();
            }

            /////////////////////////////////////////////
            MyCef3InitEssential.ClearRemainingCefMsg();
            MyCef3InitEssential.ShutDownCef3();
            //(***please note that 
            //*** we call ShutDownCef3 only in main thread ***)


            Glfw.Terminate();
        }
        //static void AddWbControlToMainWindow(Form formMain)
        //{
        //    //this test:    we create 2 web browsers

        //    cefBrowser = new MyCefBrowser(
        //        MyWindowControl.TryGetWindowControlOrRegisterIfNotExists(formMain),
        //          0, 0, formMain.Width / 2, formMain.Height, "about:blank", false);
        //    cefBrowser2 = new MyCefBrowser(
        //        MyWindowControl.TryGetWindowControlOrRegisterIfNotExists(formMain),
        //          formMain.Width / 2, 0, formMain.Width / 2, formMain.Height, "about:blank", false);
        //}
        static MyCefBrowser cefBrowser;
        //static MyCefBrowser cefBrowser2;
    }
}