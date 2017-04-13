namespace LineOfBattle
{
    interface IDrawable
    {
        DrawOptions DrawOptions { get; set; }
        void Move();
        void Draw();
    }
}
