using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caltron.ObjectModels.GLVMExecutable.Commands
{
    public enum GLVMExecutableGLUTCallType
    {
        None = 0,
        AddMenuEntry,
        AddSubMenu,
        AttachMenu,
        BitmapCharacter,
        BitmapHeight,
        BitmapLength,
        BitmapString,
        CreateWindow,
        // DisplayFunc,
        EnterGameMode,
        GameModeString,
        GetModifiers,
        GetWindow,
        HideWindow,
        // IdleFunc,
        Init,
        InitDisplayMode,
        // KeyboardFunc,
        // KeyboardUpFunc,
        LeaveGameMode,
        LeaveMainLoop,
        MainLoop,
        // MotionFunc,
        // MouseFunc,
        // MouseWheelFunc,
        // OverlayDisplayFunc,
        // PassiveMotionFunc,
        PositionWindow,
        PostRedisplay,
        // ReshapeFunc,
        ReshapeWindow,
        SetCursor,
        SetWindow,
        SetWindowTitle,
        ShowWindow,
        // SpecialFunc,
        // SpecialUpFunc,
        SwapBuffers,
        WarpPointer,
        WireCone
    }
    public class GLVMExecutableCommandGLUT : GLVMExecutableCommand
    {
        private GLVMExecutableGLUTCallType mvarFunctionName = GLVMExecutableGLUTCallType.None;
        public GLVMExecutableGLUTCallType FunctionName { get { return mvarFunctionName; } set { mvarFunctionName = value; } }

        public override object Clone()
        {
            GLVMExecutableCommandGLUT clone = new GLVMExecutableCommandGLUT();
            clone.FunctionName = mvarFunctionName;
            foreach (object obj in base.ParameterValues)
            {
                clone.ParameterValues.Add(obj);
            }
            return clone;
        }
    }
}
