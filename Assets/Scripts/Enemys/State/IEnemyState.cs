namespace Enemy
{
    // Interfaz de estados para el sistema de máquina de estados del enemigo
    public interface IEnemyState
    {
        void Enter();
        void Update();
        void Exit();
    }
}
