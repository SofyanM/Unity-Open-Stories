using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using LitJson;
using as3mbus.Story;

namespace as3mbus.Story
{
    public class Story
    {
        public string name;
        public ArrayList phases = new ArrayList();
        public Story()
        {

        }
        public Story(storyDataType storyType)
        {
            switch (storyType)
            {
                case storyDataType.BundlePath:
                    break;
                case storyDataType.DataPath:
                    break;
                case storyDataType.JsonData:
                    loadJsonStory(StoryManager.json);
                    break;
                case storyDataType.String:
                    loadJsonStory(StoryManager.stringOrPath);
                    break;
                case storyDataType.TextAsset:
                    loadJsonStory(StoryManager.textAsset);
                    break;
                default:
                    break;
            }
        }
        public string toJson()
        {
            StringBuilder sb = new StringBuilder();
            JsonWriter writer = new JsonWriter(sb);
            writer.PrettyPrint = true;
            writer.IndentValue = 4;

            toJson(writer);
            return sb.ToString();
        }
        public void toJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            writer.WritePropertyName("name");
            writer.Write(this.name);
            writer.WritePropertyName("phase");
            writer.WriteArrayStart();

            foreach (var phase in phases)
            {
                Phase fase = (Phase)phase;
                fase.toJson(writer);
            }
            writer.WriteArrayEnd();
            writer.WriteObjectEnd();
        }

        public Story(string filePath)
        {
            this.name = Path.GetFileNameWithoutExtension(filePath);
            JsonData jsonStory = JsonMapper.ToObject(File.ReadAllText(filePath));
            loadJsonStory(jsonStory);
        }

        public Story(TextAsset storyJson)
        {
            loadJsonStory(storyJson);
        }
        void loadJsonStory(TextAsset storyJson)
        {
            this.name = storyJson.name;
            loadJsonStory(storyJson.text);
        }
        void loadJsonStory(string storyJsonString)
        {
            JsonData jsonStory = JsonMapper.ToObject(storyJsonString);
            loadJsonStory(jsonStory);
        }
        void loadJsonStory(JsonData storyJson)
        {
            this.name = storyJson["name"].ToString();
            foreach (JsonData cerita in storyJson["phase"])
            {
                Phase fase = new Phase(cerita);
                this.phases.Add(fase);
            }
        }

    }

}