public interface IComponentNoEvents<ParentType, PropsType> : IInit<PropsType>
{
    ParentType Parent {get;}
}