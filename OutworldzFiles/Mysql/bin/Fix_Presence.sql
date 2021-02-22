/* Deletes Presence rows where the corresponding GridUser row does not exist (and online) */

Delete
FROM Presence
where not exists
(select * from GridUser
 where Presence.UserID = GridUser.UserID
 and GridUser.Online = 'True');



