using Integral.Domain.Models.Enums;
using Integral.WPF.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integral.WPF.Models
{
    public class ApplicationRolePermissions
    {
        public static readonly Dictionary<Role, ApplicationRolePermissions> AllPermissions;

        public static readonly ApplicationRolePermissions DefaultPermissions = new(false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, null, null);

        static ApplicationRolePermissions()
        {
            AllPermissions = new();

            ApplicationRolePermissions adminPermissions = new(true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, Enum.GetValues<LoadExtractTargetType>(), Enum.GetValues<LoadExtractTargetType>());

            ApplicationRolePermissions authorityPermissions = new(true, false, false, false, true, false, false, false, true, false, false, false, false, true, false, false, true, true, null, Enum.GetValues<LoadExtractTargetType>());

            ApplicationRolePermissions teacherPermissions = new(false, false, false, false, true, false, false, false, true, true, true, false, true, true, true, true, true, true, new LoadExtractTargetType[] { LoadExtractTargetType.Groups, LoadExtractTargetType.Meetings }, new LoadExtractTargetType[] { LoadExtractTargetType.Groups, LoadExtractTargetType.Meetings, LoadExtractTargetType.Students });

            ApplicationRolePermissions classPrincipalPermissions = new(false, false, false, false, false, false, false, false, true, false, false, false, false, true, true, true, true, true, new LoadExtractTargetType[] { LoadExtractTargetType.Meetings }, new LoadExtractTargetType[] { LoadExtractTargetType.Meetings, LoadExtractTargetType.Groups });

            AllPermissions.Add(Role.Admin, adminPermissions);
            AllPermissions.Add(Role.Teacher, teacherPermissions);
            AllPermissions.Add(Role.ClassPrincipal, classPrincipalPermissions);
            AllPermissions.Add(Role.Authority, authorityPermissions);
        }

        private ApplicationRolePermissions(bool canOpenUsersTab,
                                          bool canDeleteUser,
                                          bool canCreateUser,
                                          bool canManipulateUserRoles,
                                          bool canOpenStudentsTab,
                                          bool canCreateStudent,
                                          bool canDeleteStudent,
                                          bool canUpdateStudent,
                                          bool canOpenGroupsTab,
                                          bool canDeleteGroup,
                                          bool canCreateGroup,
                                          bool canChangeLeaderGroup,
                                          bool canModifyStudentsGroup,
                                          bool canOpenMeetingsTab,
                                          bool canCreateMeeting,
                                          bool canDeleteMeeting,
                                          bool canUpdateMeeting,
                                          bool canOpenDataManagementTab,
                                          IEnumerable<LoadExtractTargetType>? availableTargetTypesForLoad,
                                          IEnumerable<LoadExtractTargetType>? availableTargetTypesForExtract)
        {
            CanOpenUsersTab = canOpenUsersTab;
            CanDeleteUser = canDeleteUser;
            CanCreateUser = canCreateUser;
            CanManipulateUserRoles = canManipulateUserRoles;
            CanOpenStudentsTab = canOpenStudentsTab;
            CanCreateStudent = canCreateStudent;
            CanDeleteStudent = canDeleteStudent;
            CanUpdateStudent = canUpdateStudent;
            CanOpenGroupsTab = canOpenGroupsTab;
            CanDeleteGroup = canDeleteGroup;
            CanCreateGroup = canCreateGroup;
            CanChangeLeaderGroup = canChangeLeaderGroup;
            CanModifyStudentsGroup = canModifyStudentsGroup;
            CanOpenMeetingsTab = canOpenMeetingsTab;
            CanCreateMeeting = canCreateMeeting;
            CanDeleteMeeting = canDeleteMeeting;
            CanUpdateMeeting = canUpdateMeeting;
            CanOpenDataManagementTab = canOpenDataManagementTab;
            AvailableTargetTypesForLoad = availableTargetTypesForLoad;
            AvailableTargetTypesForExtract = availableTargetTypesForExtract;
        }

        public bool CanOpenUsersTab { get; set; }


        public bool CanDeleteUser { get; set; }

        public bool CanCreateUser { get; set; }

        public bool CanManipulateUserRoles { get; set; }



        public bool CanOpenStudentsTab { get; set; }


        public bool CanCreateStudent { get; set; }

        public bool CanDeleteStudent { get; set; }

        public bool CanUpdateStudent { get; set; }


        public bool CanOpenGroupsTab { get; set; }


        public bool CanDeleteGroup { get; set; }

        public bool CanCreateGroup { get; set; }

        public bool CanChangeLeaderGroup { get; set; }

        public bool CanModifyStudentsGroup { get; set; }



        public bool CanOpenMeetingsTab { get; set; }


        public bool CanCreateMeeting { get; set; }

        public bool CanDeleteMeeting { get; set; }

        public bool CanUpdateMeeting { get; set; }



        public bool CanOpenDataManagementTab { get; set; }

        public IEnumerable<LoadExtractTargetType>? AvailableTargetTypesForLoad { get; set; }

        public IEnumerable<LoadExtractTargetType>? AvailableTargetTypesForExtract { get; set; }
    }
}
