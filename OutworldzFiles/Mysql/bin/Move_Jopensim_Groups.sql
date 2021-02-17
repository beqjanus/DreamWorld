SET @opensimdb = "robust";
SET @joomladb	= "Joomla";
SET @joomlaprefix = "k0r1q_";

# groups_groups -> opensim_groups
SET @query = CONCAT("INSERT INTO ",@joomladb,".",@joomlaprefix,"opensim_group SELECT GroupID,Name,Charter,InsigniaID,FounderID,MembershipFee,OpenEnrollment,ShowInList,AllowPublish,MaturePublish,OwnerRoleID FROM ",@opensimdb,".`os_groups_groups`;");
PREPARE stmt FROM @query;
EXECUTE stmt;

# groups_principals -> opensim_group_active
SET @query = CONCAT("INSERT INTO ",@joomladb,".",@joomlaprefix,"opensim_groupactive SELECT PrincipalID,ActiveGroupID FROM ",@opensimdb,".`os_groups_principals`;");
PREPARE stmt FROM @query;
EXECUTE stmt;

# groups_invites -> opensim_groupinvite
SET @query = CONCAT("INSERT INTO ",@joomladb,".",@joomlaprefix,"opensim_groupinvite SELECT InviteID,GroupID,RoleID,PrincipalID,TMStamp FROM ",@opensimdb,".`os_groups_invites`;");
PREPARE stmt FROM @query;
EXECUTE stmt;

# groups_membership -> opensim_groupmembership
SET @query = CONCAT("INSERT INTO ",@joomladb,".",@joomlaprefix,"opensim_groupmembership SELECT GroupID,PrincipalID,SelectedRoleID,Contribution,ListInProfile,AcceptNotices FROM ",@opensimdb,".`os_groups_membership`;");
PREPARE stmt FROM @query;
EXECUTE stmt;

# groups_notices -> opensim_groupnotice
SET @query = CONCAT("INSERT INTO ",@joomladb,".",@joomlaprefix,"opensim_groupnotice SELECT GroupID,NoticeID,TMStamp,FromName,Subject,Message,NULL FROM ",@opensimdb,".`os_groups_notices`;");
PREPARE stmt FROM @query;
EXECUTE stmt;

# groups_roles -> opensim_grouprole
SET @query = CONCAT("INSERT INTO ",@joomladb,".",@joomlaprefix,"opensim_grouprole SELECT GroupID,RoleID,Name,Description,Title,Powers FROM ",@opensimdb,".`os_groups_roles`;");
PREPARE stmt FROM @query;
EXECUTE stmt;

# groups_rolemembership -> opensim_grouprolemembership
SET @query = CONCAT("INSERT INTO ",@joomladb,".",@joomlaprefix,"opensim_grouprolemembership SELECT GroupID,RoleID,PrincipalID FROM ",@opensimdb,".`os_groups_rolemembership`;");
PREPARE stmt FROM @query;
EXECUTE stmt;

DEALLOCATE PREPARE stmt; 