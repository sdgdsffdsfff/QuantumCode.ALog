using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;

namespace QuantumCode.ALog.Domain.Mapping
{
    public class ALogItemMapping : ClassMapping<ALogItem>
    {
        public ALogItemMapping()
        {
            Id(p => p.ID, map => map.Generator(Generators.Identity));

            Property(p => p.Action, map => { map.Column("action"); map.Length(50); map.NotNullable(false); });
            Property(p => p.Creator, map => { map.Column("creator"); map.Length(255); map.NotNullable(true); });
            Property(p => p.DateTimeStamp, map => { map.Column("datetimestamp"); map.NotNullable(true); });
            Property(p => p.Exception, map => { map.Column("exception"); map.Length(4000); map.NotNullable(false); });
            Property(p => p.Level, map => { map.Column("level"); map.Length(10); map.NotNullable(true); });
            Property(p => p.Message, map => { map.Column("message"); map.Length(255); map.NotNullable(true); });
            Property(p => p.Origin, map => { map.Column("origin"); map.Length(255); map.NotNullable(true); });
        }
    }
}
