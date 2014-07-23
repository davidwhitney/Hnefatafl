using System;
using Microsoft.Xna.Framework.Input;

namespace Hnefatafl.Fx
{
    public class MouseInputController : IController
    {
        private MouseState _previousState;

        public Action<Coords> OnMouseUp { get; set; }
        public Action<Coords> OnMouseDown { get; set; }
        public Action<Coords> OnLeftClick { get; set; }
        public Action<Coords> OnRightClick { get; set; }

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
                OnMouseUp(new Coords(mouseState));
                OnLeftClick(new Coords(mouseState));
            }

            if (mouseState.LeftButton == ButtonState.Pressed && _previousState.LeftButton == ButtonState.Released)
            {
                OnMouseDown(new Coords(mouseState));
            }

            _previousState = mouseState;
        }
    }
}