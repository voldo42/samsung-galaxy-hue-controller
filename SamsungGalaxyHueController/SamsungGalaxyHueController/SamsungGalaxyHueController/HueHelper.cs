using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SamsungGalaxyHueController
{
    public class HueHelper
    {
        static string bridgeUrl = "https://192.168.1.79/api/ebsbNXG4i2Ty-phG-Cnrs4MlGhQZ4y4DWAdFLkZA";
        static List<Group> groups;
        static List<Scene> scenes;

        public static List<Group> GetGroups()
        {
            List<Group> groupsList = new List<Group>();

            try
            {
                WebRequest request = WebRequest.Create($"{bridgeUrl}/groups");
                request.Credentials = CredentialCache.DefaultCredentials;

                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string groupsResponse = reader.ReadToEnd();

                var groups = JsonConvert.DeserializeObject<Dictionary<int, Group>>(groupsResponse);
                foreach (var group in groups)
                {
                    group.Value.id = group.Key;
                }

                groupsList.AddRange(groups.Values.Where(g => g.type != "LightGroup").ToList());

                reader.Close();
                response.Close();
            }
            catch (Exception)
            {

            }

            return groupsList;
        }

        public static List<Scene> GetScenes(int groupId)
        {
            List<Scene> scenesList = new List<Scene>();
            scenesList.Add(new Scene { name = "" });

            try
            {
                WebRequest request = WebRequest.Create($"{bridgeUrl}/scenes");
                request.Credentials = CredentialCache.DefaultCredentials;

                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string scenesResponse = reader.ReadToEnd();

                var scenes = JsonConvert.DeserializeObject<Dictionary<int, Scene>>(scenesResponse);
                foreach (var scene in scenes)
                {
                    scene.Value.id = scene.Key;
                }

                scenesList.AddRange(scenes.Values.Where(s => s.group == groupId).ToList());

                reader.Close();
                response.Close();
            }
            catch (Exception)
            {

            }

            scenesList.Add(new Scene { name = "" });
            return scenesList;
        }
    }
}
