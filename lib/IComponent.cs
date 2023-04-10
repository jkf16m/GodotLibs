/**
<summary>
This interface has to be used for every single declared component inside the
components folder.
</summary>
*/
public interface IComponent<ParentType, EventsType> : ICallback<EventsType>
{
    ParentType Parent {get;}

}