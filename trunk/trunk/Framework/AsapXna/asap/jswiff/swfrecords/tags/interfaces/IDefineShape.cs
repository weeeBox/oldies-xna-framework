using System;

using System.Collections.Generic;


using swiff.com.jswiff.swfrecords;

namespace swiff.com.jswiff.swfrecords.tags.interfaces
{
    public interface IDefineShape
    {
        void SetShapeBounds(Rect shapeBounds);
        Rect GetShapeBounds();
        void SetShapes(ShapeWithStyle shapes);
        ShapeWithStyle GetShapes();
    }
}