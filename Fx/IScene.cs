using Microsoft.Xna.Framework;

namespace Hnefatafl.Fx
{
    public interface IScene
    {
        void Initialize();

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        object Update(GameTime gameTime);
    }
}