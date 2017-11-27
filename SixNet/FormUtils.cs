using System.Collections.Generic;
using SixNet_GUI.Forms;
using System.Windows.Forms;
using SixNet_BBS_Data;

namespace SixNet_GUI
{
    class FormUtils
    {
        //Plumbing routines for main GUI

        private readonly DataInterface _dataInterface;

        public FormUtils(DataInterface dataInterface)
        {
            _dataInterface = dataInterface;
        }

          
        public void RefreshAccessGroups(DataGridView dg_AccessGroups)
        {
            List<AccessGroup> aglist = _dataInterface.AccessGroups();
            dg_AccessGroups.Rows.Clear();
            foreach (AccessGroup ag in aglist)
            {
                dg_AccessGroups.Rows.Add(ag.AccessGroupId, ag.AccessGroupNumber, ag.Title, ag.Description, ag.CallsPerDay, ag.MinutesPerCall, ag.Is_SysOp, ag.Flag_Remote_Maintenance);
            }

        }

        public void RefreshUsers(DataGridView dg_Users)
        {
            List<User> ulist = _dataInterface.Users();
            dg_Users.Rows.Clear();
            foreach (User u in ulist)
            {
                dg_Users.Rows.Add(u.UserId, u.Username, u.AccessLevel, u.LastConnection, u.LastDisconnection);
            }
        }

        public void RefreshMessageAreas(DataGridView dg_MessageBaseAreas)
        {
            List<MessageBaseArea> mbalist = _dataInterface.MessageBaseAreas();
            dg_MessageBaseAreas.Rows.Clear();
            foreach (MessageBaseArea mba in mbalist)
            {
                if (mba.ParentAreaId > -1)
                {
                    dg_MessageBaseAreas.Rows.Add(mba.MessageBaseAreaId, mba.Title, _dataInterface.MessageBaseAreaName(mba.ParentAreaId));
                }
                else
                {
                    dg_MessageBaseAreas.Rows.Add(mba.MessageBaseAreaId, mba.Title, "None");
                }
            }
        }

        public void RefreshMessageBases(DataGridView dg_MessageBases)
        {
            List<MessageBase> mblist = _dataInterface.MessageBases();
            dg_MessageBases.Rows.Clear();
            foreach (MessageBase mb in mblist)
            {
                if (mb.ParentArea > -1)
                {
                    dg_MessageBases.Rows.Add(mb.MessageBaseId, mb.Title, _dataInterface.MessageBaseAreaName(mb.ParentArea));
                }
                else
                {
                    dg_MessageBases.Rows.Add(mb.MessageBaseId, mb.Title, "None");
                }
            }
        }

        public  void RefreshCallLog(DataGridView dg_CallLog)
        {
            List<CallLog> cllist = _dataInterface.CallLogs();
            dg_CallLog.Rows.Clear();
            foreach (CallLog mb in cllist)
            {
                dg_CallLog.Rows.Add(mb.UserId, mb.Connected, mb.Disconnected);
            }
        }

    }
}
