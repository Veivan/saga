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
/// AttributeDef.103665 'Заголовок' [Template.76634 'Требование']
/// </summary>
public class A103665: AttributeDefCustomization<InfoObject,EntityAttribute>
{

    //CUSTOMIZATION_ORIGINAL_DIGEST:0374A0D7CACB8B143CF92AFB39BD17D547AE51CC
    //CUSTOMIZATION_BEGIN

    // Config SAGA

    /// <summary>
    /// Возвращает атрибут другого объекта для определения атрибута с типом "ссылка на другой атрибут"
    /// </summary>
    /// <param name="obj">Объект, чей атрибут вычисляем</param>
    /// <param name="attr">Определение атрибута-ссылки</param>
    /// <returns>Найденный атрибут другого объекта</returns>
    /// <remarks>Этот метод может вызываться несколько раз в ходе редактирования карточки объекта.
    /// Для корректной работы необходимо, чтобы прежде чем вернуть атрибут, объект-владелец атрибута 
    /// должен быть проверен на наличие в списке obj.BoundObjects и взят оттуда, если там уже есть такой.
    /// Код ядра проделывает эту операцию автоматически по завершении работы этой функции</remarks>
    public override EntityAttribute ResolveAttribute( InfoObject obj, AttributeDef attr )
	{
        NamedValue nval = Service.GetNamedValue(@"SAGA_confg\FactBlock_Types\tfb_Header");
        EntityAttribute result = null;
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
                result = realFactAddr.GetAttribute("TextData");
        }
        return result;
    }



    //CUSTOMIZATION_END

}

