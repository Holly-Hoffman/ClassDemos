using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Diagnostics.Eventing.Reader;

namespace LabAPIMusic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        static List<string> music = new List<string> {"Combat - Flobots", "Sway - Blue October", "Rag and Bone - Gaelic Storm",
            "As You Cry - The Hush Sound", "The Way - Fastball", "Madness - Muse", "Jump at the Sun - Scythian" };

        [HttpGet]
        public List<string> GetAllMusic()
        {
            if (music.Count == 0) return new List<string> { "You're walking without rhythm. That won't attract the worm." };
            else
            {
                return music;
            };
        }

        [HttpGet("{idx}")]
        public string GetMusicByIdx(int idx)
        {
            if (!ValidIndex(idx)) { return "You must have scratched the disc here. Nothing is playing at this track."; }
            else { return music[idx]; }
        }

        [HttpPost]
        public string AddMusic(string song)
        {
            music.Add(song);
            return "You wouldn't steal a car, but you burned that onto the disc.";
        }

        [HttpDelete("{idx}")]
        public string RemoveMusic(int idx)
        {
            music.RemoveAt(idx);
            return "Thank goodness, that one was stuck in my head too long.";
        }

        [HttpPut]
        public string UpdateMusic(int idx, string uMusic)
        {
            if (!ValidIndex(idx)) { return "You can do nothing to improve upon this silence."; }
            else
            {
                music[idx] = uMusic;
                return "Nice collab remix!";
            }
        }

        private bool ValidIndex(int idx)
        {
            bool isValid = true;
            if (idx < 0 || idx >= music.Count)
            {
                isValid = false;
            }
            return isValid;
        }


    }
}
