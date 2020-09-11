using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for DataTypes
/// </summary>
public class DataTypes
{
    public DataTypes()
    {

    }

    public enum MYSQLDataType
    {
        INT,
        VARCHAR,
        TEXT,
        DATE,
        TINYINT,
        SMALLINT,
        MEDIUMINT,
        BIGINT,
        DECIMAL,
        FLOAT,
        DOUBLE,
        REAL,
        BIT,
        BOOL,
        SERIAL,
        DATETIME,
        TIME,
        TIMESTAMP,
        YEAR,
        CHAR,
        TINYTEXT,
        MEDIUMTEXT,
        LONGTEXT,
        BINARY,
        VARBINARY,
        BLOB,
        TINYBLOB,
        MEDIUMBLOB,
        LONGBLOB,
        ENUM,
        SET,
        GEOMETRY ,
        POINT ,
        LINESTRING ,
        POLYGON ,
        MULTIPOINT ,
        MULTILINESTRING ,
        MULTIPOLYGON ,
        GEOMETRYCOLLECTION
    }

    public enum MSSQLDataType
    {
        BIGINT,
        BINARY,
        BIT,
        CHAR,
        DATETIME,
        DECIMAL,
        FLOAT,
        IMAGE,
        INT,
        MONEY,
        NCHAR,
        NTEXT,
        NUMERIC,
        NVARCHAR,
        REAL,
        SMALLDATETIME,
        SMALLINT,
        SMALLMONEY,
        SQL_VARIANT,
        TEXT,
        TIMESTAMP,
        TINYINT,
        UNIQUEIDENTIFIER,
        VARBINARY,
        VARCHAR,
        XML
    }
}
