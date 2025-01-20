using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
namespace StackOverflowProject.ServiceLayer
{
   public static class MapperExtenstions
    {
        public static void IgnoreUnmmapedProperties(TypeMap map,IMappingExpression mappingExpression)
        {
            foreach (string propname in map.GetUnmappedPropertyNames())
            {
                if (map.SourceType.GetProperty(propname)!=null )
                {
                    mappingExpression.ForMember(propname, opt => opt.Ignore());
                }
                if (map.DestinationType.GetProperty(propname) != null)
                {
                    mappingExpression.ForMember(propname, opt => opt.Ignore());
                }
            }
            
        }

        public static void IgnoreUnmmaped(this IProfileExpression profile)
        {
            profile.ForAllMaps(IgnoreUnmmapedProperties);
        }
    }
}
