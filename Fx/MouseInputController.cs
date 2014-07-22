using System;
using Microsoft.Xna.Framework.Input;

namespace Hnefatafl.Fx
{
    public class MouseInputController : IController
    {
        private MouseState _previousState;

        public Action<MouseState> OnMouseUp { get; set; }
        public Action<MouseState> OnMouseDown { get; set; }
        public Action<MouseState> OnLeftClick { get; set; }
        public Action<MouseState> OnRightClick { get; set; }

        public MouseInputController()
        {
            OnMouseUp = (ms => { });
            OnMouseDown = (ms => { });
            OnLeftClick = (ms => { });
            OnRightClick = (ms => { });
        }

        public void ReadInput()
        {
            var mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Released && _previousState.LeftButton == ButtonState.Pressed)
            {
                OnMouseUp(mouseState);
                OnLeftClick(mouseState);
            }

            if (mouseState.LeftButton == ButtonState.Pressed && _previousState.LeftButton == ButtonState.Released)
            {
                OnMouseDown(mouseState);
            }

            _previousState = mouseState;
        }
    }
}