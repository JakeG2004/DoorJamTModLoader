using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoorJam.Common.Players
{
    public class DoorJamPlayer : ModPlayer
    {
        private bool isOnDoor = false;
        Random rnd = new Random();
        public override void PreUpdate()
        {
            DoDoorJam();
        }

        public void DoDoorJam()
        {
            // Convert player's position to tile coordinates
            int tileX = (int)(Player.position.X / 16f);
            int tileY = (int)(Player.position.Y / 16f);

            // Get the current player tile
            Tile playerTile = Main.tile[tileX, tileY];

            //handle player moving next to door
            if(IsNextToDoor(playerTile) && !isOnDoor)
            {
                // 20% chance to jam your fingers
                if(rnd.Next(0, 20) == 0)
                {
                    Player.Hurt(PlayerDeathReason.ByCustomReason(Player.name + " jammed their finger in the door"), 10, 0, false, false, -1, true, 0f, 0f, 0f);
                }

                isOnDoor = true;
            }

            //handle player moving away from door
            if(!IsNextToDoor(playerTile) && isOnDoor)
            {
                //Main.NewText("Not next to door");
                isOnDoor = false;
            }
        }

        //return true if player is next to door, false otherwise
        public bool IsNextToDoor(Tile curTile)
        {
            if(curTile.TileType == TileID.OpenDoor || curTile.TileType == TileID.ClosedDoor)
            {
                return true;
            }

            return false;
        }
    }
}
