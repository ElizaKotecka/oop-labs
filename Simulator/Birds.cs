namespace Simulator;

public class Birds : Animals
{
    public bool CanFly { get; set; } = true;

    public override char MapSymbol => CanFly ? 'B' : 'b';

    public override string Info
    {
        get
        {
            string flyStatus = CanFly ? "(fly+)" : "(fly-)";
            return $"{Description} {flyStatus} <{Size}>";
        }
    }
    public override void Go(Direction direction)
    {
        if (map is null) return;

        Point nextPoint;
        if (CanFly)
        {
            // Lot o 2 pola
            nextPoint = map.Next(point, direction);
            nextPoint = map.Next(nextPoint, direction);
        }
        else
        {
            // Ruch po skosie o 1 pole
            nextPoint = map.NextDiagonal(point, direction);
        }

        map.Move(this, point, nextPoint);
        point = nextPoint;
    }
}