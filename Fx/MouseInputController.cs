using System;
using Microsoft.Xna.Framework.Input;

namespace Hnefatafl.Fx
{
    public class MouseInputController : IController
    {
        private readonly Action<MouseState> _onLeftClick;

        private MouseState _previousState;

        public MouseInputController(Action<MouseState> onLeftClick)
        {
            _onLeftClick = onLeftClick ?? (ms => { });
        }

        public void ReadInput()
        {
            var mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Released && _previousState.LeftButton == ButtonState.Pressed)
            {
                _onLeftClick(mouseState);
            }

            _previousState = mouseState;
        }
    }
}