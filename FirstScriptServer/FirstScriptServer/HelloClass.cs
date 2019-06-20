using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using System;
using System.Collections.Generic;

namespace FirstScriptServer
{
    public class HelloClass : BaseScript
    {
        public HelloClass()
        {
            EventHandlers["onClientResourceStart"] += new Action<string>(OnClientResourceStart);
            EventHandlers["onClientResourceStart"] += new Action<string>(SetSkin);
        }

        private void OnClientResourceStart(string resourceName)
        {
            if (GetCurrentResourceName() != resourceName) return;

            RegisterCommand("car", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                // account for the argument not being passed
                var model = "adder";
                if (args.Count > 0)
                {
                    model = args[0].ToString();                    
                }
                
                // check if the model actually exists
                // assumes the directive `using static CitizenFX.Core.Native.API;`
                var hash = (uint)GetHashKey(model);
                if (!IsModelInCdimage(hash) || !IsModelAVehicle(hash))
                {
                    TriggerEvent("chat:addMessage", new
                    {
                        color = new[] { 255, 0, 0 },
                        args = new[] { "[CarSpawner]", $"It might have been a good thing that you tried to spawn a {model}. Who even wants their spawning to actually ^*succeed?" }
                    });
                    return;
                }

                // create the vehicle
                var vehicle = await World.CreateVehicle(model, Game.PlayerPed.Position, Game.PlayerPed.Heading);

                // set the player ped into the vehicle and driver seat
                Game.PlayerPed.SetIntoVehicle(vehicle, VehicleSeat.Driver);

                // tell the player
                TriggerEvent("chat:addMessage", new
                {
                    color = new[] { 255, 0, 0 },
                    args = new[] { "[CarSpawner]", $"Woohoo! Enjoy your new ^*{model}!" }
                });
            }), false);
        }
        private void SetSkin(string resourceName)
        {
            if (GetCurrentResourceName() != resourceName) return;

            RegisterCommand("setskin", new Action<int, List<object>, string>(async(source, args, raw) => {
                string skin = null;
                if(args.Count > 0)
                {
                    skin = args[0].ToString();
                }
                
                var hash = (uint)GetHashKey(skin);
                //if (!IsModelAPed(hash))
                //{
                //    TriggerEvent("chat:addMessage", new
                //    {
                //        color = new[] { 255, 0, 0 },
                //        args = new[] { "[SKIN]", $"Скин {skin} не существует. HASH: {hash}" }
                //    });
                //    return;
                //}
                //await Game.Player.ChangeModel(new Model(skin));

            }), false);
        }
    }
}