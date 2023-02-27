using UnityEngine;

namespace DoomScroll_V2.Common
{
    public interface IDirectory
    {
        public GameObject Dir { get; }
        public CustomButton Btn { get; }
        public CustomText Label { get; }
        public void DisplayContent();
        public string PrintDirectory();
    }
}
