using System.Collections.Generic;
using System.Linq;
using Cruel.Interfaces;

namespace Cruel.Nodes
{
   public static class SegmentExtensions
   {
      public static Segment Segment(this IEnumerable<ISegment> segments)
      {
         var array = segments.Select(s => s.Segment).ToArray();
         switch (array.Length)
         {
            case 0:
               return Cruel.Segment.Empty;
            case 1:
               return array[0];
            default:
            {
               var min = array[0];
               var max = array[array.Length - 1];
               var index = min.Index;
               var length = array.Select(s => s.Length).Sum();
               var startLine = min.StartLine;
               var startColumn = min.StartColumn;
               var endLine = max.EndLine;
               var endColumn = max.EndColumn;

               return new Segment(index, length, startLine, startColumn, endLine, endColumn);
            }
         }
      }
   }
}