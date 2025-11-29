using FluentAssertions;
using Waterloo.Repository.Station;

namespace Waterloo.Repository.Unit.Tests;
public class StationUnitTest
{
    private readonly StationRepository _stationRepository = new();

    [Fact]
    public void StationRepository_GetByLine_Bakerloo_ShouldReturnAllStationsOnLine()
    {
        var result = _stationRepository.GetByLine(Guid.Parse("e6d7a23e-0f5f-4a90-a1c7-4e8e48c64823"));

        result.Should().NotBeNullOrEmpty();
        result.Should().Contain(x => x.Name == "Baker Street");
        result.Should().Contain(x => x.Name == "Charing Cross");
        result.Should().Contain(x => x.Name == "Edgware Road");
        result.Should().Contain(x => x.Name == "Elephant & Castle");
        result.Should().Contain(x => x.Name == "Embankment");
        result.Should().Contain(x => x.Name == "Harlesden");
        result.Should().Contain(x => x.Name == "Harrow & Wealdstone");
        result.Should().Contain(x => x.Name == "Kensal Green");
        result.Should().Contain(x => x.Name == "Kenton");
        result.Should().Contain(x => x.Name == "Kilburn Park");
        result.Should().Contain(x => x.Name == "Lambeth North");
        result.Should().Contain(x => x.Name == "Maida Vale");
        result.Should().Contain(x => x.Name == "Marylebone");
        result.Should().Contain(x => x.Name == "North Wembley");
        result.Should().Contain(x => x.Name == "Oxford Circus");
        result.Should().Contain(x => x.Name == "Paddington");
        result.Should().Contain(x => x.Name == "Piccadilly Circus");
        result.Should().Contain(x => x.Name == "Queen's Park");
        result.Should().Contain(x => x.Name == "Regent's Park");
        result.Should().Contain(x => x.Name == "South Kenton");
        result.Should().Contain(x => x.Name == "Stonebridge Park");
        result.Should().Contain(x => x.Name == "Warwick Avenue");
        result.Should().Contain(x => x.Name == "Waterloo");
        result.Should().Contain(x => x.Name == "Wembley Central");
        result.Should().Contain(x => x.Name == "Willesden Junction");
        result.Count().Should().Be(25);
    }

    [Fact]
    public void StationRepository_GetByLine_Central_ShouldReturnAllStationsOnLine()
    {
        var result = _stationRepository.GetByLine(Guid.Parse("c7f7c41a-03d2-4a79-9e8e-b55b1b5a056e"));

        result.Should().NotBeNullOrEmpty();
        result.Should().Contain(x => x.Name == "Bank");
        result.Should().Contain(x => x.Name == "Barkingside");
        result.Should().Contain(x => x.Name == "Bethnal Green");
        result.Should().Contain(x => x.Name == "Bond Street");
        result.Should().Contain(x => x.Name == "Buckhurst Hill");
        result.Should().Contain(x => x.Name == "Chancery Lane");
        result.Should().Contain(x => x.Name == "Chigwell");
        result.Should().Contain(x => x.Name == "Debden");
        result.Should().Contain(x => x.Name == "Ealing Broadway");
        result.Should().Contain(x => x.Name == "East Acton");
        result.Should().Contain(x => x.Name == "Epping");
        result.Should().Contain(x => x.Name == "Fairlop");
        result.Should().Contain(x => x.Name == "Gants Hill");
        result.Should().Contain(x => x.Name == "Grange Hill");
        result.Should().Contain(x => x.Name == "Greenford");
        result.Should().Contain(x => x.Name == "Hainault");
        result.Should().Contain(x => x.Name == "Hanger Lane");
        result.Should().Contain(x => x.Name == "Holborn");
        result.Should().Contain(x => x.Name == "Holland Park");
        result.Should().Contain(x => x.Name == "Lancaster Gate");
        result.Should().Contain(x => x.Name == "Leyton");
        result.Should().Contain(x => x.Name == "Leytonstone");
        result.Should().Contain(x => x.Name == "Liverpool Street");
        result.Should().Contain(x => x.Name == "Loughton");
        result.Should().Contain(x => x.Name == "Marble Arch");
        result.Should().Contain(x => x.Name == "Mile End");
        result.Should().Contain(x => x.Name == "Newbury Park");
        result.Should().Contain(x => x.Name == "North Acton");
        result.Should().Contain(x => x.Name == "Northolt");
        result.Should().Contain(x => x.Name == "Notting Hill Gate");
        result.Should().Contain(x => x.Name == "Oxford Circus");
        result.Should().Contain(x => x.Name == "Perivale");
        result.Should().Contain(x => x.Name == "Queensway");
        result.Should().Contain(x => x.Name == "Redbridge");
        result.Should().Contain(x => x.Name == "Roding Valley");
        result.Should().Contain(x => x.Name == "Ruislip Gardens");
        result.Should().Contain(x => x.Name == "Shepherd's Bush");
        result.Should().Contain(x => x.Name == "Snaresbrook");
        result.Should().Contain(x => x.Name == "South Ruislip");
        result.Should().Contain(x => x.Name == "South Woodford");
        result.Should().Contain(x => x.Name == "St.Paul's");
        result.Should().Contain(x => x.Name == "Stratford");
        result.Should().Contain(x => x.Name == "Theydon Bois");
        result.Should().Contain(x => x.Name == "Tottenham Court Road");
        result.Should().Contain(x => x.Name == "Wanstead");
        result.Should().Contain(x => x.Name == "West Acton");
        result.Should().Contain(x => x.Name == "West Ruislip");
        result.Should().Contain(x => x.Name == "White City");
        result.Should().Contain(x => x.Name == "Woodford");
        result.Count().Should().Be(49);
    }

    [Fact]
    public void StationRepository_GetByLine_Circle_ShouldReturnAllStationsOnLine()
    {
        var result = _stationRepository.GetByLine(Guid.Parse("5e8c1a94-5f0c-4d4d-8c4b-07bba9f5eb54"));

        result.Should().NotBeNullOrEmpty();
        result.Should().Contain(x => x.Name == "Aldgate");
        result.Should().Contain(x => x.Name == "Baker Street");
        result.Should().Contain(x => x.Name == "Barbican");
        result.Should().Contain(x => x.Name == "Bayswater");
        result.Should().Contain(x => x.Name == "Blackfriars");
        result.Should().Contain(x => x.Name == "Cannon Street");
        result.Should().Contain(x => x.Name == "Edgware Road");
        result.Should().Contain(x => x.Name == "Embankment");
        result.Should().Contain(x => x.Name == "Euston Square");
        result.Should().Contain(x => x.Name == "Farringdon");
        result.Should().Contain(x => x.Name == "Gloucester Road");
        result.Should().Contain(x => x.Name == "Goldhawk Road");
        result.Should().Contain(x => x.Name == "Great Portland Street");
        result.Should().Contain(x => x.Name == "Hammersmith");
        result.Should().Contain(x => x.Name == "High Street Kensington");
        result.Should().Contain(x => x.Name == "King's Cross St.Pancras");
        result.Should().Contain(x => x.Name == "Ladbroke Grove");
        result.Should().Contain(x => x.Name == "Latimer Road");
        result.Should().Contain(x => x.Name == "Liverpool Street");
        result.Should().Contain(x => x.Name == "Mansion House");
        result.Should().Contain(x => x.Name == "Monument");
        result.Should().Contain(x => x.Name == "Moorgate");
        result.Should().Contain(x => x.Name == "Notting Hill Gate");
        result.Should().Contain(x => x.Name == "Paddington");
        result.Should().Contain(x => x.Name == "Royal Oak");
        result.Should().Contain(x => x.Name == "Shepherd's Bush Market");
        result.Should().Contain(x => x.Name == "Sloane Square");
        result.Should().Contain(x => x.Name == "South Kensington");
        result.Should().Contain(x => x.Name == "St.James's Park");
        result.Should().Contain(x => x.Name == "Temple");
        result.Should().Contain(x => x.Name == "Tower Hill");
        result.Should().Contain(x => x.Name == "Victoria");
        result.Should().Contain(x => x.Name == "Westbourne Park");
        result.Should().Contain(x => x.Name == "Westminster");
        result.Should().Contain(x => x.Name == "Wood Lane");

        result.Count().Should().Be(35);
    }

    [Fact]
    public void StationRepository_GetByLine_District_ShouldReturnAllStationsOnLine()
    {
        var result = _stationRepository.GetByLine(Guid.Parse("8c3a4d59-f2e0-46a8-9f56-ec27eaffded9"));

        result.Should().NotBeNullOrEmpty();
        result.Should().Contain(x => x.Name == "Acton Town");
        result.Should().Contain(x => x.Name == "Aldgate East");
        result.Should().Contain(x => x.Name == "Barking");
        result.Should().Contain(x => x.Name == "Barons Court");
        result.Should().Contain(x => x.Name == "Bayswater");
        result.Should().Contain(x => x.Name == "Becontree");
        result.Should().Contain(x => x.Name == "Blackfriars");
        result.Should().Contain(x => x.Name == "Bow Road");
        result.Should().Contain(x => x.Name == "Bromley By Bow");
        result.Should().Contain(x => x.Name == "Cannon Street");
        result.Should().Contain(x => x.Name == "Chiswick Park");
        result.Should().Contain(x => x.Name == "Dagenham East");
        result.Should().Contain(x => x.Name == "Dagenham Heathway");
        result.Should().Contain(x => x.Name == "Ealing Broadway");
        result.Should().Contain(x => x.Name == "Ealing Common");
        result.Should().Contain(x => x.Name == "Earl's Court");
        result.Should().Contain(x => x.Name == "East Ham");
        result.Should().Contain(x => x.Name == "East Putney");
        result.Should().Contain(x => x.Name == "Edgware Road");
        result.Should().Contain(x => x.Name == "Elm Park");
        result.Should().Contain(x => x.Name == "Embankment");
        result.Should().Contain(x => x.Name == "Fulham Broadway");
        result.Should().Contain(x => x.Name == "Gloucester Road");
        result.Should().Contain(x => x.Name == "Gunnersbury");
        result.Should().Contain(x => x.Name == "Hammersmith");
        result.Should().Contain(x => x.Name == "High Street Kensington");
        result.Should().Contain(x => x.Name == "Hornchurch");
        result.Should().Contain(x => x.Name == "Kensington (Olympia)");
        result.Should().Contain(x => x.Name == "Kew Gardens");
        result.Should().Contain(x => x.Name == "Mansion House");
        result.Should().Contain(x => x.Name == "Mile End");
        result.Should().Contain(x => x.Name == "Monument");
        result.Should().Contain(x => x.Name == "Notting Hill Gate");
        result.Should().Contain(x => x.Name == "Paddington");
        result.Should().Contain(x => x.Name == "Parsons Green");
        result.Should().Contain(x => x.Name == "Plaistow");
        result.Should().Contain(x => x.Name == "Putney Bridge");
        result.Should().Contain(x => x.Name == "Ravenscourt Park");
        result.Should().Contain(x => x.Name == "Richmond");
        result.Should().Contain(x => x.Name == "Sloane Square");
        result.Should().Contain(x => x.Name == "South Kensington");
        result.Should().Contain(x => x.Name == "Southfields");
        result.Should().Contain(x => x.Name == "St.James's Park");
        result.Should().Contain(x => x.Name == "Stamford Brook");
        result.Should().Contain(x => x.Name == "Stepney Green");
        result.Should().Contain(x => x.Name == "Temple");
        result.Should().Contain(x => x.Name == "Tower Hill");
        result.Should().Contain(x => x.Name == "Turnham Green");
        result.Should().Contain(x => x.Name == "Upminster Bridge");
        result.Should().Contain(x => x.Name == "Upminster");
        result.Should().Contain(x => x.Name == "Upney");
        result.Should().Contain(x => x.Name == "Upton Park");
        result.Should().Contain(x => x.Name == "Victoria");
        result.Should().Contain(x => x.Name == "West Brompton");
        result.Should().Contain(x => x.Name == "West Ham");
        result.Should().Contain(x => x.Name == "West Kensington");
        result.Should().Contain(x => x.Name == "Westminster");
        result.Should().Contain(x => x.Name == "Whitechapel");
        result.Should().Contain(x => x.Name == "Wimbledon");
        result.Should().Contain(x => x.Name == "Wimbledon Park");

        result.Count().Should().Be(60);
    }

    [Fact]
    public void StationRepository_GetByLine_DLR_ShouldReturnAllStationsOnLine()
    {
        var result = _stationRepository.GetByLine(Guid.Parse("85b9e52c-697b-4db8-876f-600423ffe176"));

        result.Should().NotBeNullOrEmpty();

        result.Should().Contain(x => x.Name == "Abbey Road");
        result.Should().Contain(x => x.Name == "All Saints");
        result.Should().Contain(x => x.Name == "Bank");
        result.Should().Contain(x => x.Name == "Beckton");
        result.Should().Contain(x => x.Name == "Beckton Park");
        result.Should().Contain(x => x.Name == "Blackwall");
        result.Should().Contain(x => x.Name == "Bow Church");
        result.Should().Contain(x => x.Name == "Canning Town");
        result.Should().Contain(x => x.Name == "Canary Wharf");
        result.Should().Contain(x => x.Name == "Crossharbour");
        result.Should().Contain(x => x.Name == "Custom House");
        result.Should().Contain(x => x.Name == "Cutty Sark");
        result.Should().Contain(x => x.Name == "Cyprus");
        result.Should().Contain(x => x.Name == "Deptford Bridge");
        result.Should().Contain(x => x.Name == "Devons Road");
        result.Should().Contain(x => x.Name == "East India");
        result.Should().Contain(x => x.Name == "Elverson Road");
        result.Should().Contain(x => x.Name == "Gallions Reach");
        result.Should().Contain(x => x.Name == "Greenwich");
        result.Should().Contain(x => x.Name == "Heron Quays");
        result.Should().Contain(x => x.Name == "Island Gardens");
        result.Should().Contain(x => x.Name == "King George V");
        result.Should().Contain(x => x.Name == "Langdon Park");
        result.Should().Contain(x => x.Name == "Lewisham");
        result.Should().Contain(x => x.Name == "Limehouse");
        result.Should().Contain(x => x.Name == "London City Airport");
        result.Should().Contain(x => x.Name == "Mudchute");
        result.Should().Contain(x => x.Name == "Pontoon Dock");
        result.Should().Contain(x => x.Name == "Poplar");
        result.Should().Contain(x => x.Name == "Prince Regent");
        result.Should().Contain(x => x.Name == "Pudding Mill Lane");
        result.Should().Contain(x => x.Name == "Royal Albert");
        result.Should().Contain(x => x.Name == "Royal Victoria");
        result.Should().Contain(x => x.Name == "Shadwell");
        result.Should().Contain(x => x.Name == "South Quay");
        result.Should().Contain(x => x.Name == "Star Lane");
        result.Should().Contain(x => x.Name == "Stratford");
        result.Should().Contain(x => x.Name == "Stratford High Street");
        result.Should().Contain(x => x.Name == "Stratford International");
        result.Should().Contain(x => x.Name == "Tower Gateway");
        result.Should().Contain(x => x.Name == "Westferry");
        result.Should().Contain(x => x.Name == "West Ham");
        result.Should().Contain(x => x.Name == "West India Quay");
        result.Should().Contain(x => x.Name == "West Silvertown");
        result.Should().Contain(x => x.Name == "Woolwich Arsenal");


        result.Count().Should().Be(45);
    }

    [Fact]
    public void StationRepository_GetByLine_Elizabeth_ShouldReturnAllStationsOnLine()
    {
        var result = _stationRepository.GetByLine(Guid.Parse("d232ac77-6032-4658-aed8-e47f89b79353"));

        result.Should().NotBeNullOrEmpty();
        result.Should().Contain(x => x.Name == "Abbey Wood");
        result.Should().Contain(x => x.Name == "Acton Main Line");
        result.Should().Contain(x => x.Name == "Bond Street");
        result.Should().Contain(x => x.Name == "Brentwood");
        result.Should().Contain(x => x.Name == "Burnham");
        result.Should().Contain(x => x.Name == "Canary Wharf");
        result.Should().Contain(x => x.Name == "Chadwell Heath");
        result.Should().Contain(x => x.Name == "Custom House");
        result.Should().Contain(x => x.Name == "Ealing Broadway");
        result.Should().Contain(x => x.Name == "Farringdon");
        result.Should().Contain(x => x.Name == "Forest Gate");
        result.Should().Contain(x => x.Name == "Gidea Park");
        result.Should().Contain(x => x.Name == "Goodmayes");
        result.Should().Contain(x => x.Name == "Hanwell");
        result.Should().Contain(x => x.Name == "Harold Wood");
        result.Should().Contain(x => x.Name == "Hayes & Harlington");
        result.Should().Contain(x => x.Name == "Heathrow Terminal 4");
        result.Should().Contain(x => x.Name == "Heathrow Terminal 5");
        result.Should().Contain(x => x.Name == "Heathrow Terminals 2 & 3");
        result.Should().Contain(x => x.Name == "Ilford");
        result.Should().Contain(x => x.Name == "Iver");
        result.Should().Contain(x => x.Name == "Langley");
        result.Should().Contain(x => x.Name == "Liverpool Street");
        result.Should().Contain(x => x.Name == "Maidenhead");
        result.Should().Contain(x => x.Name == "Manor Park");
        result.Should().Contain(x => x.Name == "Maryland");
        result.Should().Contain(x => x.Name == "Paddington");
        result.Should().Contain(x => x.Name == "Reading");
        result.Should().Contain(x => x.Name == "Romford");
        result.Should().Contain(x => x.Name == "Seven Kings");
        result.Should().Contain(x => x.Name == "Shenfield");
        result.Should().Contain(x => x.Name == "Slough");
        result.Should().Contain(x => x.Name == "Southall");
        result.Should().Contain(x => x.Name == "Stratford");
        result.Should().Contain(x => x.Name == "Taplow");
        result.Should().Contain(x => x.Name == "Tottenham Court Road");
        result.Should().Contain(x => x.Name == "Twyford");
        result.Should().Contain(x => x.Name == "West Drayton");
        result.Should().Contain(x => x.Name == "West Ealing");
        result.Should().Contain(x => x.Name == "Whitechapel");
        result.Should().Contain(x => x.Name == "Woolwich");
        result.Count().Should().Be(41);
    }

    [Fact]
    public void StationRepository_GetByLine_HammersmithAndCity_ShouldReturnAllStationsOnLine()
    {
        var result = _stationRepository.GetByLine(Guid.Parse("14a64b9a-9c65-4d49-8c38-4c1782a73c0a"));

        result.Should().NotBeNullOrEmpty();
        result.Should().Contain(x => x.Name == "Aldgate East");
        result.Should().Contain(x => x.Name == "Baker Street");
        result.Should().Contain(x => x.Name == "Barbican");
        result.Should().Contain(x => x.Name == "Barking");
        result.Should().Contain(x => x.Name == "Bow Road");
        result.Should().Contain(x => x.Name == "Bromley By Bow");
        result.Should().Contain(x => x.Name == "East Ham");
        result.Should().Contain(x => x.Name == "Edgware Road");
        result.Should().Contain(x => x.Name == "Euston Square");
        result.Should().Contain(x => x.Name == "Farringdon");
        result.Should().Contain(x => x.Name == "Goldhawk Road");
        result.Should().Contain(x => x.Name == "Great Portland Street");
        result.Should().Contain(x => x.Name == "Hammersmith");
        result.Should().Contain(x => x.Name == "King's Cross St.Pancras");
        result.Should().Contain(x => x.Name == "Ladbroke Grove");
        result.Should().Contain(x => x.Name == "Latimer Road");
        result.Should().Contain(x => x.Name == "Liverpool Street");
        result.Should().Contain(x => x.Name == "Mile End");
        result.Should().Contain(x => x.Name == "Moorgate");
        result.Should().Contain(x => x.Name == "Paddington");
        result.Should().Contain(x => x.Name == "Plaistow");
        result.Should().Contain(x => x.Name == "Royal Oak");
        result.Should().Contain(x => x.Name == "Shepherd's Bush Market");
        result.Should().Contain(x => x.Name == "Stepney Green");
        result.Should().Contain(x => x.Name == "Upton Park");
        result.Should().Contain(x => x.Name == "West Ham");
        result.Should().Contain(x => x.Name == "Westbourne Park");
        result.Should().Contain(x => x.Name == "Whitechapel");
        result.Should().Contain(x => x.Name == "Wood Lane");

        result.Count().Should().Be(29);
    }

    [Fact]
    public void StationRepository_GetByLine_Jubilee_ShouldReturnAllStationsOnLine()
    {
        var result = _stationRepository.GetByLine(Guid.Parse("2f0c75a5-8149-49b7-9cc6-32e4a5246d7f"));

        result.Should().NotBeNullOrEmpty();
        result.Should().Contain(x => x.Name == "Baker Street");
        result.Should().Contain(x => x.Name == "Bermondsey");
        result.Should().Contain(x => x.Name == "Bond Street");
        result.Should().Contain(x => x.Name == "Canada Water");
        result.Should().Contain(x => x.Name == "Canary Wharf");
        result.Should().Contain(x => x.Name == "Canning Town");
        result.Should().Contain(x => x.Name == "Canons Park");
        result.Should().Contain(x => x.Name == "Dollis Hill");
        result.Should().Contain(x => x.Name == "Finchley Road");
        result.Should().Contain(x => x.Name == "Green Park");
        result.Should().Contain(x => x.Name == "Kilburn");
        result.Should().Contain(x => x.Name == "Kingsbury");
        result.Should().Contain(x => x.Name == "London Bridge");
        result.Should().Contain(x => x.Name == "Neasden");
        result.Should().Contain(x => x.Name == "North Greenwich");
        result.Should().Contain(x => x.Name == "Queensbury");
        result.Should().Contain(x => x.Name == "Southwark");
        result.Should().Contain(x => x.Name == "St.John's Wood");
        result.Should().Contain(x => x.Name == "Stanmore");
        result.Should().Contain(x => x.Name == "Stratford");
        result.Should().Contain(x => x.Name == "Swiss Cottage");
        result.Should().Contain(x => x.Name == "Waterloo");
        result.Should().Contain(x => x.Name == "Wembley Park");
        result.Should().Contain(x => x.Name == "West Ham");
        result.Should().Contain(x => x.Name == "West Hampstead");
        result.Should().Contain(x => x.Name == "Westminster");
        result.Should().Contain(x => x.Name == "Willesden Green");
        result.Count().Should().Be(27);
    }

    [Fact]
    public void StationRepository_GetByLine_Liberty_ShouldReturnAllStationsOnLine()
    {
        var result = _stationRepository.GetByLine(Guid.Parse("1ef96e79-2dab-43b3-b931-6bf9a0495b22"));

        result.Should().NotBeNullOrEmpty();
        result.Should().Contain(x => x.Name == "Romford");
        result.Should().Contain(x => x.Name == "Emerson Park");
        result.Should().Contain(x => x.Name == "Upminster");

        result.Count().Should().Be(3);
    }

    [Fact]
    public void StationRepository_GetByLine_Lioness_ShouldReturnAllStationsOnLine()
    {
        var result = _stationRepository.GetByLine(Guid.Parse("1bc14562-671e-4cb8-99c0-f77f3458a03d"));

        result.Should().NotBeNullOrEmpty();
        result.Should().Contain(x => x.Name == "Bushey");
        result.Should().Contain(x => x.Name == "Carpenders Park");
        result.Should().Contain(x => x.Name == "Harlesden");
        result.Should().Contain(x => x.Name == "Harrow & Wealdstone");
        result.Should().Contain(x => x.Name == "Hatch End");
        result.Should().Contain(x => x.Name == "Headstone Lane");
        result.Should().Contain(x => x.Name == "Kensal Green");
        result.Should().Contain(x => x.Name == "Kenton");
        result.Should().Contain(x => x.Name == "Kilburn High Road");
        result.Should().Contain(x => x.Name == "Euston");
        result.Should().Contain(x => x.Name == "North Wembley");
        result.Should().Contain(x => x.Name == "Queen's Park");
        result.Should().Contain(x => x.Name == "South Hampstead");
        result.Should().Contain(x => x.Name == "South Kenton");
        result.Should().Contain(x => x.Name == "Stonebridge Park");
        result.Should().Contain(x => x.Name == "Watford High Street");
        result.Should().Contain(x => x.Name == "Watford Junction");
        result.Should().Contain(x => x.Name == "Wembley Central");
        result.Should().Contain(x => x.Name == "Willesden Junction");

        result.Count().Should().Be(19);
    }

    [Fact]
    public void StationRepository_GetByLine_Mildmay_ShouldReturnAllStationsOnLine()
    {
        var result = _stationRepository.GetByLine(Guid.Parse("73d9c9bd-fabd-49f7-9c67-dbb752f07453"));

        result.Should().NotBeNullOrEmpty();
        result.Should().Contain(x => x.Name == "Acton Central");
        result.Should().Contain(x => x.Name == "Brondesbury Park");
        result.Should().Contain(x => x.Name == "Brondesbury");
        result.Should().Contain(x => x.Name == "Caledonian Road & Barnsbury");
        result.Should().Contain(x => x.Name == "Camden Road");
        result.Should().Contain(x => x.Name == "Canonbury");
        result.Should().Contain(x => x.Name == "Clapham Junction");
        result.Should().Contain(x => x.Name == "Dalston Kingsland");
        result.Should().Contain(x => x.Name == "Finchley Road & Frognal");
        result.Should().Contain(x => x.Name == "Gospel Oak");
        result.Should().Contain(x => x.Name == "Gunnersbury");
        result.Should().Contain(x => x.Name == "Hackney Central");
        result.Should().Contain(x => x.Name == "Hackney Wick");
        result.Should().Contain(x => x.Name == "Hampstead Heath");
        result.Should().Contain(x => x.Name == "Highbury & Islington");
        result.Should().Contain(x => x.Name == "Homerton");
        result.Should().Contain(x => x.Name == "Imperial Wharf");
        result.Should().Contain(x => x.Name == "Kensal Rise");
        result.Should().Contain(x => x.Name == "Kensington (Olympia)");
        result.Should().Contain(x => x.Name == "Kentish Town West");
        result.Should().Contain(x => x.Name == "Kew Gardens");
        result.Should().Contain(x => x.Name == "Richmond");
        result.Should().Contain(x => x.Name == "Shepherd's Bush");
        result.Should().Contain(x => x.Name == "South Acton");
        result.Should().Contain(x => x.Name == "Stratford");
        result.Should().Contain(x => x.Name == "West Brompton");
        result.Should().Contain(x => x.Name == "West Hampstead");
        result.Should().Contain(x => x.Name == "Willesden Junction");

        result.Count().Should().Be(28);
    }

    [Fact]
    public void StationRepository_GetByLine_Metropolitan_ShouldReturnAllStationsOnLine()
    {
        var result = _stationRepository.GetByLine(Guid.Parse("9e3a7f43-b6c4-4f12-9a72-ffbe2d15b9e6"));

        result.Should().NotBeNullOrEmpty();
        result.Should().Contain(x => x.Name == "Aldgate");
        result.Should().Contain(x => x.Name == "Amersham");
        result.Should().Contain(x => x.Name == "Baker Street");
        result.Should().Contain(x => x.Name == "Barbican");
        result.Should().Contain(x => x.Name == "Chalfont & Latimer");
        result.Should().Contain(x => x.Name == "Chesham");
        result.Should().Contain(x => x.Name == "Chorleywood");
        result.Should().Contain(x => x.Name == "Croxley");
        result.Should().Contain(x => x.Name == "Eastcote");
        result.Should().Contain(x => x.Name == "Euston Square");
        result.Should().Contain(x => x.Name == "Farringdon");
        result.Should().Contain(x => x.Name == "Finchley Road");
        result.Should().Contain(x => x.Name == "Great Portland Street");
        result.Should().Contain(x => x.Name == "Harrow On The Hill");
        result.Should().Contain(x => x.Name == "Hillingdon");
        result.Should().Contain(x => x.Name == "Ickenham");
        result.Should().Contain(x => x.Name == "King's Cross St.Pancras");
        result.Should().Contain(x => x.Name == "Liverpool Street");
        result.Should().Contain(x => x.Name == "Moor Park");
        result.Should().Contain(x => x.Name == "Moorgate");
        result.Should().Contain(x => x.Name == "North Harrow");
        result.Should().Contain(x => x.Name == "Northwick Park");
        result.Should().Contain(x => x.Name == "Northwood");
        result.Should().Contain(x => x.Name == "Northwood Hills");
        result.Should().Contain(x => x.Name == "Pinner");
        result.Should().Contain(x => x.Name == "Preston Road");
        result.Should().Contain(x => x.Name == "Rayners Lane");
        result.Should().Contain(x => x.Name == "Rickmansworth");
        result.Should().Contain(x => x.Name == "Ruislip");
        result.Should().Contain(x => x.Name == "Ruislip Manor");
        result.Should().Contain(x => x.Name == "Uxbridge");
        result.Should().Contain(x => x.Name == "Watford");
        result.Should().Contain(x => x.Name == "Wembley Park");
        result.Should().Contain(x => x.Name == "West Harrow");

        result.Count().Should().Be(34);
    }

    [Fact]
    public void StationRepository_GetByLine_Northern_ShouldReturnAllStationsOnLine()
    {
        var result = _stationRepository.GetByLine(Guid.Parse("62e93d5d-cc67-4c42-8ff5-24582f89d624"));

        result.Should().Contain(x => x.Name == "Angel");
        result.Should().Contain(x => x.Name == "Archway");
        result.Should().Contain(x => x.Name == "Balham");
        result.Should().Contain(x => x.Name == "Bank");
        result.Should().Contain(x => x.Name == "Battersea Power Station");
        result.Should().Contain(x => x.Name == "Belsize Park");
        result.Should().Contain(x => x.Name == "Borough");
        result.Should().Contain(x => x.Name == "Brent Cross");
        result.Should().Contain(x => x.Name == "Burnt Oak");
        result.Should().Contain(x => x.Name == "Camden Town");
        result.Should().Contain(x => x.Name == "Chalk Farm");
        result.Should().Contain(x => x.Name == "Charing Cross");
        result.Should().Contain(x => x.Name == "Clapham Common");
        result.Should().Contain(x => x.Name == "Clapham North");
        result.Should().Contain(x => x.Name == "Clapham South");
        result.Should().Contain(x => x.Name == "Colindale");
        result.Should().Contain(x => x.Name == "Colliers Wood");
        result.Should().Contain(x => x.Name == "East Finchley");
        result.Should().Contain(x => x.Name == "Edgware");
        result.Should().Contain(x => x.Name == "Elephant & Castle");
        result.Should().Contain(x => x.Name == "Embankment");
        result.Should().Contain(x => x.Name == "Euston");
        result.Should().Contain(x => x.Name == "Finchley Central");
        result.Should().Contain(x => x.Name == "Golders Green");
        result.Should().Contain(x => x.Name == "Goodge Street");
        result.Should().Contain(x => x.Name == "Hampstead");
        result.Should().Contain(x => x.Name == "Hendon Central");
        result.Should().Contain(x => x.Name == "High Barnet");
        result.Should().Contain(x => x.Name == "Highgate");
        result.Should().Contain(x => x.Name == "Kennington");
        result.Should().Contain(x => x.Name == "Kentish Town");
        result.Should().Contain(x => x.Name == "King's Cross St.Pancras");
        result.Should().Contain(x => x.Name == "Leicester Square");
        result.Should().Contain(x => x.Name == "London Bridge");
        result.Should().Contain(x => x.Name == "Mill Hill East");
        result.Should().Contain(x => x.Name == "Moorgate");
        result.Should().Contain(x => x.Name == "Morden");
        result.Should().Contain(x => x.Name == "Mornington Crescent");
        result.Should().Contain(x => x.Name == "Nine Elms");
        result.Should().Contain(x => x.Name == "Old Street");
        result.Should().Contain(x => x.Name == "Oval");
        result.Should().Contain(x => x.Name == "South Wimbledon");
        result.Should().Contain(x => x.Name == "Stockwell");
        result.Should().Contain(x => x.Name == "Tooting Bec");
        result.Should().Contain(x => x.Name == "Tooting Broadway");
        result.Should().Contain(x => x.Name == "Tottenham Court Road");
        result.Should().Contain(x => x.Name == "Totteridge & Whetstone");
        result.Should().Contain(x => x.Name == "Tufnell Park");
        result.Should().Contain(x => x.Name == "Warren Street");
        result.Should().Contain(x => x.Name == "Waterloo");
        result.Should().Contain(x => x.Name == "West Finchley");
        result.Should().Contain(x => x.Name == "Woodside Park");

        result.Count().Should().Be(52);
    }

    [Fact]
    public void StationRepository_GetByLine_Piccadilly_ShouldReturnAllStationsOnLine()
    {
        var result = _stationRepository.GetByLine(Guid.Parse("6c9e1d2c-845e-4d08-885f-17b9cf28e154"));

        result.Should().Contain(x => x.Name == "Acton Town");
        result.Should().Contain(x => x.Name == "Alperton");
        result.Should().Contain(x => x.Name == "Arnos Grove");
        result.Should().Contain(x => x.Name == "Arsenal");
        result.Should().Contain(x => x.Name == "Barons Court");
        result.Should().Contain(x => x.Name == "Boston Manor");
        result.Should().Contain(x => x.Name == "Bounds Green");
        result.Should().Contain(x => x.Name == "Caledonian Road");
        result.Should().Contain(x => x.Name == "Cockfosters");
        result.Should().Contain(x => x.Name == "Covent Garden");
        result.Should().Contain(x => x.Name == "Ealing Common");
        result.Should().Contain(x => x.Name == "Earl's Court");
        result.Should().Contain(x => x.Name == "Eastcote");
        result.Should().Contain(x => x.Name == "Finsbury Park");
        result.Should().Contain(x => x.Name == "Gloucester Road");
        result.Should().Contain(x => x.Name == "Green Park");
        result.Should().Contain(x => x.Name == "Hammersmith");
        result.Should().Contain(x => x.Name == "Hatton Cross");
        result.Should().Contain(x => x.Name == "Heathrow Terminal 4");
        result.Should().Contain(x => x.Name == "Heathrow Terminal 5");
        result.Should().Contain(x => x.Name == "Heathrow Terminals 2 & 3");
        result.Should().Contain(x => x.Name == "Hillingdon");
        result.Should().Contain(x => x.Name == "Holborn");
        result.Should().Contain(x => x.Name == "Holloway Road");
        result.Should().Contain(x => x.Name == "Hounslow Central");
        result.Should().Contain(x => x.Name == "Hounslow East");
        result.Should().Contain(x => x.Name == "Hounslow West");
        result.Should().Contain(x => x.Name == "Hyde Park Corner");
        result.Should().Contain(x => x.Name == "Ickenham");
        result.Should().Contain(x => x.Name == "King's Cross St.Pancras");
        result.Should().Contain(x => x.Name == "Knightsbridge");
        result.Should().Contain(x => x.Name == "Leicester Square");
        result.Should().Contain(x => x.Name == "Manor House");
        result.Should().Contain(x => x.Name == "North Ealing");
        result.Should().Contain(x => x.Name == "Northfields");
        result.Should().Contain(x => x.Name == "Oakwood");
        result.Should().Contain(x => x.Name == "Osterley");
        result.Should().Contain(x => x.Name == "Park Royal");
        result.Should().Contain(x => x.Name == "Piccadilly Circus");
        result.Should().Contain(x => x.Name == "Rayners Lane");
        result.Should().Contain(x => x.Name == "Ruislip");
        result.Should().Contain(x => x.Name == "Ruislip Manor");
        result.Should().Contain(x => x.Name == "Russell Square");
        result.Should().Contain(x => x.Name == "South Ealing");
        result.Should().Contain(x => x.Name == "South Harrow");
        result.Should().Contain(x => x.Name == "South Kensington");
        result.Should().Contain(x => x.Name == "Southgate");
        result.Should().Contain(x => x.Name == "Sudbury Hill");
        result.Should().Contain(x => x.Name == "Sudbury Town");
        result.Should().Contain(x => x.Name == "Turnham Green");
        result.Should().Contain(x => x.Name == "Turnpike Lane");
        result.Should().Contain(x => x.Name == "Uxbridge");
        result.Should().Contain(x => x.Name == "Wood Green");

        result.Count().Should().Be(53);
    }

    [Fact]
    public void StationRepository_GetByLine_Suffragette_ShouldReturnAllStationsOnLine()
    {
        var result = _stationRepository.GetByLine(Guid.Parse("2cb7f0a1-fa5f-43bd-af91-18dd83623284"));

        result.Should().Contain(x => x.Name == "Barking");
        result.Should().Contain(x => x.Name == "Barking Riverside");
        result.Should().Contain(x => x.Name == "Blackhorse Road");
        result.Should().Contain(x => x.Name == "Crouch Hill");
        result.Should().Contain(x => x.Name == "Gospel Oak");
        result.Should().Contain(x => x.Name == "Harringay Green Lanes");
        result.Should().Contain(x => x.Name == "Leyton Midland Road");
        result.Should().Contain(x => x.Name == "Leytonstone High Road");
        result.Should().Contain(x => x.Name == "South Tottenham");
        result.Should().Contain(x => x.Name == "Upper Holloway");
        result.Should().Contain(x => x.Name == "Walthamstow Queens Road");
        result.Should().Contain(x => x.Name == "Wanstead Park");
        result.Should().Contain(x => x.Name == "Woodgrange Park");

        result.Count().Should().Be(13);
    }

    [Fact]
    public void StationRepository_GetByLine_Tram_ShouldReturnAllStationsOnLine()
    {
        var result = _stationRepository.GetByLine(Guid.Parse("e466a866-6404-4a7d-a55d-a3e21d328e52"));

        result.Should().Contain(x => x.Name == "Addington Village");
        result.Should().Contain(x => x.Name == "Addiscombe");
        result.Should().Contain(x => x.Name == "Ampere Way");
        result.Should().Contain(x => x.Name == "Arena");
        result.Should().Contain(x => x.Name == "Avenue Road");
        result.Should().Contain(x => x.Name == "Beckenham Junction");
        result.Should().Contain(x => x.Name == "Beckenham Road");
        result.Should().Contain(x => x.Name == "Beddington Lane");
        result.Should().Contain(x => x.Name == "Belgrave Walk");
        result.Should().Contain(x => x.Name == "Birkbeck");
        result.Should().Contain(x => x.Name == "Blackhorse Lane");
        result.Should().Contain(x => x.Name == "Centrale");
        result.Should().Contain(x => x.Name == "Church Street");
        result.Should().Contain(x => x.Name == "Coombe Lane");
        result.Should().Contain(x => x.Name == "Dundonald Road");
        result.Should().Contain(x => x.Name == "East Croydon");
        result.Should().Contain(x => x.Name == "Elmers End");
        result.Should().Contain(x => x.Name == "Fieldway");
        result.Should().Contain(x => x.Name == "George Street");
        result.Should().Contain(x => x.Name == "Gravel Hill");
        result.Should().Contain(x => x.Name == "Harrington Road");
        result.Should().Contain(x => x.Name == "King Henry's Drive");
        result.Should().Contain(x => x.Name == "Lebanon Road");
        result.Should().Contain(x => x.Name == "Lloyd Park");
        result.Should().Contain(x => x.Name == "Merton Park");
        result.Should().Contain(x => x.Name == "Mitcham Junction");
        result.Should().Contain(x => x.Name == "Mitcham");
        result.Should().Contain(x => x.Name == "Morden Road");
        result.Should().Contain(x => x.Name == "New Addington");
        result.Should().Contain(x => x.Name == "Phipps Bridge");
        result.Should().Contain(x => x.Name == "Reeves Corner");
        result.Should().Contain(x => x.Name == "Sandilands");
        result.Should().Contain(x => x.Name == "Therapia Lane");
        result.Should().Contain(x => x.Name == "Waddon Marsh");
        result.Should().Contain(x => x.Name == "Wandle Park");
        result.Should().Contain(x => x.Name == "Wellesley Road");
        result.Should().Contain(x => x.Name == "West Croydon");
        result.Should().Contain(x => x.Name == "Wimbledon");
        result.Should().Contain(x => x.Name == "Woodside");

        result.Count().Should().Be(39);
    }

    [Fact]
    public void StationRepository_GetByLine_Victoria_ShouldReturnAllStationsOnLine()
    {
        var result = _stationRepository.GetByLine(Guid.Parse("9c834a1e-8a34-4c1e-943e-6f37b8e1e9d4"));

        result.Should().Contain(x => x.Name == "Blackhorse Road");
        result.Should().Contain(x => x.Name == "Brixton");
        result.Should().Contain(x => x.Name == "Euston");
        result.Should().Contain(x => x.Name == "Finsbury Park");
        result.Should().Contain(x => x.Name == "Green Park");
        result.Should().Contain(x => x.Name == "Highbury & Islington");
        result.Should().Contain(x => x.Name == "King's Cross St.Pancras");
        result.Should().Contain(x => x.Name == "Oxford Circus");
        result.Should().Contain(x => x.Name == "Pimlico");
        result.Should().Contain(x => x.Name == "Seven Sisters");
        result.Should().Contain(x => x.Name == "Stockwell");
        result.Should().Contain(x => x.Name == "Tottenham Hale");
        result.Should().Contain(x => x.Name == "Vauxhall");
        result.Should().Contain(x => x.Name == "Victoria");
        result.Should().Contain(x => x.Name == "Walthamstow Central");
        result.Should().Contain(x => x.Name == "Warren Street");

        result.Count().Should().Be(16);
    }

    [Fact]
    public void StationRepository_GetByLine_WaterlooAndCity_ShouldReturnAllStationsOnLine()
    {
        var result = _stationRepository.GetByLine(Guid.Parse("73c2b92d-ef29-4bbf-9f60-57a1f8ab7f50"));

        result.Should().Contain(x => x.Name == "Bank");
        result.Should().Contain(x => x.Name == "Waterloo");

        result.Count().Should().Be(2);
    }

    [Fact]
    public void StationRepository_GetByLine_Weaver_ShouldReturnAllStationsOnLine()
    {
        var result = _stationRepository.GetByLine(Guid.Parse("0b3d9ef1-fdd6-4db8-8d37-34e9c6ab8385"));

        result.Should().Contain(x => x.Name == "Bethnal Green");
        result.Should().Contain(x => x.Name == "Bruce Grove");
        result.Should().Contain(x => x.Name == "Bush Hill Park");
        result.Should().Contain(x => x.Name == "Cambridge Heath");
        result.Should().Contain(x => x.Name == "Cheshunt");
        result.Should().Contain(x => x.Name == "Chingford");
        result.Should().Contain(x => x.Name == "Clapton");
        result.Should().Contain(x => x.Name == "Edmonton Green");
        result.Should().Contain(x => x.Name == "Enfield Town");
        result.Should().Contain(x => x.Name == "Hackney Downs");
        result.Should().Contain(x => x.Name == "Highams Park");
        result.Should().Contain(x => x.Name == "London Fields");
        result.Should().Contain(x => x.Name == "Liverpool Street");
        result.Should().Contain(x => x.Name == "Rectory Road");
        result.Should().Contain(x => x.Name == "Seven Sisters");
        result.Should().Contain(x => x.Name == "Silver Street");
        result.Should().Contain(x => x.Name == "Southbury");
        result.Should().Contain(x => x.Name == "St.James Street");
        result.Should().Contain(x => x.Name == "Stamford Hill");
        result.Should().Contain(x => x.Name == "Stoke Newington");
        result.Should().Contain(x => x.Name == "Theobalds Grove");
        result.Should().Contain(x => x.Name == "Turkey Street");
        result.Should().Contain(x => x.Name == "Walthamstow Central");
        result.Should().Contain(x => x.Name == "White Hart Lane");
        result.Should().Contain(x => x.Name == "Wood Street");

        result.Count().Should().Be(25);
    }

    [Fact]
    public void StationRepository_GetByLine_Windrush_ShouldReturnAllStationsOnLine()
    {
        var result = _stationRepository.GetByLine(Guid.Parse("2f9d4b4c-c001-4be2-b02f-b227c1a7d84b"));

        result.Should().Contain(x => x.Name == "Anerley");
        result.Should().Contain(x => x.Name == "Brockley");
        result.Should().Contain(x => x.Name == "Canada Water");
        result.Should().Contain(x => x.Name == "Canonbury");
        result.Should().Contain(x => x.Name == "Clapham High Street");
        result.Should().Contain(x => x.Name == "Clapham Junction");
        result.Should().Contain(x => x.Name == "Crystal Palace");
        result.Should().Contain(x => x.Name == "Dalston Junction");
        result.Should().Contain(x => x.Name == "Denmark Hill");
        result.Should().Contain(x => x.Name == "Forest Hill");
        result.Should().Contain(x => x.Name == "Haggerston");
        result.Should().Contain(x => x.Name == "Highbury & Islington");
        result.Should().Contain(x => x.Name == "Honor Oak Park");
        result.Should().Contain(x => x.Name == "Hoxton");
        result.Should().Contain(x => x.Name == "New Cross");
        result.Should().Contain(x => x.Name == "New Cross Gate");
        result.Should().Contain(x => x.Name == "Norwood Junction");
        result.Should().Contain(x => x.Name == "Peckham Rye");
        result.Should().Contain(x => x.Name == "Penge West");
        result.Should().Contain(x => x.Name == "Queens Road Peckham");
        result.Should().Contain(x => x.Name == "Rotherhithe");
        result.Should().Contain(x => x.Name == "Shadwell");
        result.Should().Contain(x => x.Name == "Shoreditch High Street");
        result.Should().Contain(x => x.Name == "Surrey Quays");
        result.Should().Contain(x => x.Name == "Sydenham");
        result.Should().Contain(x => x.Name == "Wandsworth Road");
        result.Should().Contain(x => x.Name == "Wapping");
        result.Should().Contain(x => x.Name == "West Croydon");
        result.Should().Contain(x => x.Name == "Whitechapel");


        result.Count().Should().Be(29);
    }

    [Fact]
    public void StationRepository_GetByStationId_Should_Return_Station()
    {
        var result = _stationRepository.GetStationById(Guid.Parse("a359263f-448b-42dd-a05f-660aa6ef53ec"));

        result.Id.Should().Be(Guid.Parse("a359263f-448b-42dd-a05f-660aa6ef53ec"));
        result.Name.Should().Be("Camden Town");
    }

    [Fact]
    public void StationRepository_GetStationsById_Should_Return_All_Stations()
    {
        var stationIds = new List<Guid>()
        {
            Guid.Parse("ec8f48bc-23d5-4788-9251-f3fa1ff8a5d4"),
            Guid.Parse("36ce6d95-4979-4511-aef0-aa8f7b031838"),
            Guid.Parse("a359263f-448b-42dd-a05f-660aa6ef53ec")
        };

        var expectedStations = new List<Model.Station>()
        {
            new(Guid.Parse("ec8f48bc-23d5-4788-9251-f3fa1ff8a5d4"), "Burnt Oak"),
            new(Guid.Parse("36ce6d95-4979-4511-aef0-aa8f7b031838"), "Caledonian Road"),
            new(Guid.Parse("a359263f-448b-42dd-a05f-660aa6ef53ec"), "Camden Town")
        };

        var stations = _stationRepository.GetStationsById(stationIds);

        stations.IsSuccess.Should().BeTrue();
        stations.Value.Should().BeEquivalentTo(expectedStations);
    }

    [Fact]
    public void StationRepository_GetStationsById_With_Incorrect_Id_Should_Fail()
    {
        var stationIds = new List<Guid>()
        {
            Guid.Parse("a6eaa521-f8b0-4d94-ba06-eb3aac222bde"),
            Guid.Parse("36ce6d95-4979-4511-aef0-aa8f7b031838"),
            Guid.Parse("a359263f-448b-42dd-a05f-660aa6ef53ec")
        };

        var stations = _stationRepository.GetStationsById(stationIds);
        stations.IsFailure.Should().BeTrue();
        stations.Error.Should().Be($"Could not find station with id: {stationIds.First()}");
    }

    [Fact]
    public void LineRepository_GeStationsById_With_Same_Station_Should_Return_Line_Once()
    {
        var stationIds = new List<Guid>()
        {
            Guid.Parse("ec8f48bc-23d5-4788-9251-f3fa1ff8a5d4"),
            Guid.Parse("ec8f48bc-23d5-4788-9251-f3fa1ff8a5d4"),
            Guid.Parse("a359263f-448b-42dd-a05f-660aa6ef53ec")
        };

        var expectedStations = new List<Model.Station>()
        {
            new(Guid.Parse("ec8f48bc-23d5-4788-9251-f3fa1ff8a5d4"), "Burnt Oak"),
            new(Guid.Parse("a359263f-448b-42dd-a05f-660aa6ef53ec"), "Camden Town")
        };

        var stations = _stationRepository.GetStationsById(stationIds);

        stations.IsSuccess.Should().BeTrue();
        stations.Value.Should().BeEquivalentTo(expectedStations);
    }

    [Theory]
    [InlineData("Watford Junction")]
    [InlineData("Watford Junction Rail Station")]
    [InlineData("watford jUnction")]
    [InlineData("watford jUnction Rail Station")]
    public void StationRepository_GetStationByName_CorrectStation(string name)
    {
        var result = _stationRepository.GetStationByName(name);

        result.Name.Should().Be("Watford Junction");
        result.Id.Should().Be(Guid.Parse("455d4830-bd0d-4962-8869-6443c4c8c452"));
    }

    [Fact]
    public void StationRepository_GetByToStations_Northern_Battersea_Power_Station_ShouldReturnCorrectStations()
    {
        var lineId = Guid.Parse("62e93d5d-cc67-4c42-8ff5-24582f89d624");
        var stationId = Guid.Parse("668e4ceb-b98d-46d4-8d1f-6abb248fb577");

        var result = _stationRepository.GetByToStation(lineId, stationId);

        result.Should().Contain(x => x.Name == "Nine Elms");
        result.Should().Contain(x => x.Name == "Kennington");
        result.Should().Contain(x => x.Name == "Waterloo");
        result.Should().Contain(x => x.Name == "Embankment");
        result.Should().Contain(x => x.Name == "Charing Cross");
        result.Should().Contain(x => x.Name == "Leicester Square");
        result.Should().Contain(x => x.Name == "Tottenham Court Road");
        result.Should().Contain(x => x.Name == "Goodge Street");
        result.Should().Contain(x => x.Name == "Warren Street");
        result.Should().Contain(x => x.Name == "Euston");
        result.Should().Contain(x => x.Name == "Mornington Crescent");
        result.Should().Contain(x => x.Name == "Camden Town");
        result.Should().Contain(x => x.Name == "Chalk Farm");
        result.Should().Contain(x => x.Name == "Belsize Park");
        result.Should().Contain(x => x.Name == "Hampstead");
        result.Should().Contain(x => x.Name == "Golders Green");
        result.Should().Contain(x => x.Name == "Brent Cross");
        result.Should().Contain(x => x.Name == "Hendon Central");
        result.Should().Contain(x => x.Name == "Colindale");
        result.Should().Contain(x => x.Name == "Burnt Oak");
        result.Should().Contain(x => x.Name == "Edgware");
        result.Should().Contain(x => x.Name == "Kentish Town");
        result.Should().Contain(x => x.Name == "Tufnell Park");
        result.Should().Contain(x => x.Name == "Archway");
        result.Should().Contain(x => x.Name == "Highgate");
        result.Should().Contain(x => x.Name == "East Finchley");
        result.Should().Contain(x => x.Name == "Finchley Central");
        result.Should().Contain(x => x.Name == "West Finchley");
        result.Should().Contain(x => x.Name == "Woodside Park");
        result.Should().Contain(x => x.Name == "Totteridge & Whetstone");
        result.Should().Contain(x => x.Name == "High Barnet");

        result.Count().Should().Be(31);
    }
}
