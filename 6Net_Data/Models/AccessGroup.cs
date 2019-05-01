using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Net_Data.Models
{
    public class AccessGroup : TitledModel
    {
        //CREATE TABLE `bbs`.`AccessGroups` (
          //`Id` INT NOT NULL,
          //`Title` NVARCHAR(25) NULL,
          //`Description` NVARCHAR(255) NULL,
          //`CallsPerDay` INT NULL,
          //`MinutesPerCall` INT NULL,
          //`AllowSysop` BIT NULL,
          //PRIMARY KEY(`Id`));

        public int CallsPerDay { get; set; }
        public int MinutesPerCall { get; set; }
        public bool AllowRemoteMaintenance { get; set; }
        public bool AllowSysOp { get; set; }

        public ICollection<UserAccessGroup> UserAccessGroups { get; set; }
    }
}
