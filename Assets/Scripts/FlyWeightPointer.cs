public static class FlyWeightPointer
{
    public static readonly ThrowingObjectFlyWeight TObject = new ThrowingObjectFlyWeight()
    {
        speed = 7.5f,
    };

    public static readonly PassengerFlyweigth passenger = new PassengerFlyweigth()
    {
        viewRadius = 0.9f,
        seeTime = 1.5f,
        iDontBuyTime = 3f,
    };
}