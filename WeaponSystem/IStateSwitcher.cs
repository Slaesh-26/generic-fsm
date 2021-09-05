namespace FSM
{
    public interface IStateSwitcher
    {
        public void SwitchState<T>() where T : State;

        public void SwitchToPreviousState();
    }
}