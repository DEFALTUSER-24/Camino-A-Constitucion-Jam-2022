public static class FlyWeightPointer
{
    public static readonly CandyFlyweight candy = new CandyFlyweight()
    {
        speed = 7.5f,
    };

    public static readonly PassengerFlyweigth passenger = new PassengerFlyweigth()
    {
        viewRadius = 0.9f,
    };
}