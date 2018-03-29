using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Drawing;
using System.Data.Common;
using System.Diagnostics;
using System.Windows.Forms;
using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;
using ProgramSoyuz.Common;
using ProgramSoyuz.Common.GuiLib;
using ProgramSoyuz.DataModel;
using ProgramSoyuz.PLM.Scripting;
using Wintellect.PowerCollections;
using HttpUtility = System.Web.HttpUtility;
using ProgramSoyuz.Common.Sound;
using Ionic.Zlib;
using Ionic.Zip;

using Attribute = ProgramSoyuz.PLM.Scripting.Attribute;
using Cursor = System.Windows.Forms.Cursor;


/// <summary>
/// ScriptingTask.17044 'Saga Commons'
/// </summary>
public class SagaLibCommons: ScriptLibraryCustomization
{

    //CUSTOMIZATION_ORIGINAL_DIGEST:2102F1C24EA74ECC833C1896D1FB017D16F4B3C1
    //CUSTOMIZATION_BEGIN

    // Config SAGA

    /// <summary>
    /// Возвращает значение атрибута "TextData" фактического блока, дочернего к объекту obj
    /// </summary>
    /// <param name="obj">Объект, для которого ищутся фактические данные</param>
    /// <param name="nval">Тип блока фактических данных</param>
    /// <returns>значение атрибута "TextData" фактического блока</returns>
    /// <remarks>В поле FactAddr может быть ссылка как на одиночный блок фактических данных,
    /// так и ссылка на блок метаданных типа "Коллекция"
    /// </remarks>
    public static string GetFactData(InfoObject obj, NamedValue nval)
    {
        var FactData = "";
        var FactAddr = obj.GetInfoObject("FactAddr");
        if (FactAddr != null)
        {
            InfoObject realFactAddr = null;
            if (FactAddr.Template.NameKey == "FactBlock")
            {
                // одиночный блок фактических данных
                if (FactAddr.GetValue<NamedValue>("BlockType") == nval)
                    realFactAddr = FactAddr;
            }
            else
            {
                // ссылка на блок метаданных типа "Коллекция"
                realFactAddr = FactAddr.Children.Where(o => o.GetValue<NamedValue>("BlockType") == nval).FirstOrDefault();
            }
            if (realFactAddr != null)
                FactData = realFactAddr.GetString("TextData");
        }
        return FactData;
    }

    //CUSTOMIZATION_END

}

