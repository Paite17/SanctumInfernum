using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public List<Unit> currentPartyMembers;


    public void SetPartyMember(Unit member)
    {
        currentPartyMembers.Add(member);
    }

    public void RemovePartyMember(Unit member)
    {
        if (!member.KeyMember)
        {
            currentPartyMembers.Remove(member);
        }
        else
        {
            // TODO: UI popup or something saying you can't remove them
            return;
        }
    }
}
