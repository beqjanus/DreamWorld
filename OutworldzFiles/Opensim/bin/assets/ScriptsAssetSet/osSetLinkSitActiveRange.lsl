/*
osSetLinkSitActiveRange(integer linkNumber, float range)
sets a limit on how far a avatar can be to have a sit request accepted, or disable sits

    linkNumber is link number of the prim to change or one of LINK_SET,LINK_ROOT, LINK_ALL_OTHERS,LINK_ALL_CHILDREN or LINK_THIS
    range > 0: if a avatar if far from the prim by more than that value, a sit request is silent ignored
    range == 0: disables this limit. Region default is used. Current that is unlimited if a sit target is set or physics can sit the avatar, otherwise 10m
    range < 0: sits are disabled. Requests are silently ignored 

this value is stored on the prim, even if the script is removed
Threat Level 	None
Permissions 	Use of this function is always allowed by default
Delay 	0 seconds
Example(s)
*/

// Example of osSetLinkSitActiveRange
 
vector target = <0.0, 0.3, 0.55>;
vector rotate = <0.0, 0.0, 90.0>;
float active_link_range = 5.0;
integer link = 2;
key avatar;
 
debug(string name)
{
    active_link_range = osGetLinkSitActiveRange(link);
    llOwnerSay("active_link_range for avatar " + name + " is " + (string)active_link_range);
}
 
default
{
    state_entry()
    {
        llUnSit(llAvatarOnSitTarget());
        llSetClickAction(CLICK_ACTION_SIT);
        llLinkSitTarget(link, target, llEuler2Rot((rotate * DEG_TO_RAD)));
        osSetLinkSitActiveRange(link, active_link_range);
    }
 
    changed(integer change)
    {
        if (change & CHANGED_LINK)
        {
            key user = llAvatarOnSitTarget();
 
            if (user != NULL_KEY)
            {
                avatar = user;
                debug(osKey2Name(avatar));
            }
 
            else if (user == NULL_KEY)
            {
                debug(osKey2Name(avatar));
                avatar = NULL_KEY;
            }
 
            else
            {
                llResetScript();
            }
        }
    }
}