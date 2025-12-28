namespace SimWeb.Pages;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Simulator;
using Simulator.Maps;

public class SimulationModel : PageModel
{
    // przechowujemy biezaca ture (z logu) do wyswietlenia
    public TurnLog CurrentTurn { get; private set; } = default!;

    // numer tury
    public int TurnIndex { get; private set; }

    // calkowita liczba tur
    public int TotalTurns { get; private set; }

    // rozmiary mapy zeby wiedziec jak rysowac
    public int SizeX { get; private set; }
    public int SizeY { get; private set; }

    public void OnGet()
    {
        // pobieramy indeks tury z sesji (jesli nie ma, to 0)
        TurnIndex = HttpContext.Session.GetInt32("TurnIndex") ?? 0;

        // przygotowujemy symulacje
        var simulationLog = GetSimulationLog();

        // zabezpieczamy zakresy (zeby nie wyjsc poza liste)
        TotalTurns = simulationLog.TurnLogs.Count - 1;
        if (TurnIndex < 0) TurnIndex = 0;
        if (TurnIndex > TotalTurns) TurnIndex = TotalTurns;

        // pobieramy dane dla aktualnej tury
        CurrentTurn = simulationLog.TurnLogs[TurnIndex];
        SizeX = simulationLog.SizeX;
        SizeY = simulationLog.SizeY;
    }

    // obsluga przyciskow (next / prev)
    public IActionResult OnPost(string action)
    {
        // pobieramy aktualny stan
        int current = HttpContext.Session.GetInt32("TurnIndex") ?? 0;

        // musimy wiedziec ile jest max tur, wiec tworzymy log
        var log = GetSimulationLog();
        int max = log.TurnLogs.Count - 1;

        // zmieniamy licznik w zaleznosci od przycisku
        if (action == "next")
        {
            if (current < max) current++;
        }
        else if (action == "prev")
        {
            if (current > 0) current--;
        }

        // zapisujemy nowy stan w sesji
        HttpContext.Session.SetInt32("TurnIndex", current);

        // przeladowujemy strone (wzorzec PRG - Post Redirect Get)
        return RedirectToPage();
    }

    // metoda pomocnicza tworzaca konkretna symulacje
    private SimulationLog GetSimulationLog()
    {
        SmallTorusMap map = new(8, 6);
        List<IMappable> mappables = [
            new Orc("Gorbag"),
            new Elf("Elandor"),
            new Animals { Description = "Rabbits", Size = 10 },
            new Birds { Description = "Eagles", CanFly = true },
            new Birds { Description = "Ostriches", CanFly = false }
        ];
        List<Point> points = [
            new(2, 2), // orc
            new(3, 1), // elf
            new(5, 5), // rabbit
            new(7, 3), // eagle
            new(0, 4)  // emu
        ];
        string moves = "dlrludluddlrulrdullu"; //20 rund

        Simulation simulation = new(map, mappables, points, moves);

        // log (symulacja wykona sie w tle cala od razu)
        return new SimulationLog(simulation);
    }
}