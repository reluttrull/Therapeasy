using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Therapeasy.Enums
{
    public enum BodyRegion
    {
        [Display(Name = "Hand and wrist")]
        HandAndWrist = 0,
        [Display(Name = "Elbow")]
        Elbow = 1,
        [Display(Name = "Shoulder")]
        Shoulder = 2,
        [Display(Name = "Neck")]
        Neck = 3,
        [Display(Name = "Back")]
        Back = 4,
        [Display(Name = "Hip")]
        Hip = 5,
        [Display(Name = "Knee")]
        Knee = 6,
        [Display(Name = "Foot and ankle")]
        FootAndAnkle = 7,
        [Display(Name = "Upper body: other")]
        UpperBodyOther = 8,
        [Display(Name = "Lower body: other")]
        LowerBodyOther = 9
    }
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var memberInfo = enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault();
            if (memberInfo != null)
            {
                var displayAttribute = memberInfo.GetCustomAttribute<DisplayAttribute>();
                if (displayAttribute != null)
                {
                    return displayAttribute.Name;
                }
            }
            return enumValue.ToString(); // fallback default
        }
    }
}