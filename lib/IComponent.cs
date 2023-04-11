/**
<summary>
This interface has to be used for every single declared component inside the
components folder.
</summary>
*/
public interface IComponent<ParentType, PropsType, EventsType> : ICallback<EventsType>
{
    ParentType Parent {get;}

}