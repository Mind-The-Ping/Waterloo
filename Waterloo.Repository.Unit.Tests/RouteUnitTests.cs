using FluentAssertions;
using Waterloo.Repository.Route;

namespace Waterloo.Repository.Unit.Tests;
public class RouteUnitTests
{
    private readonly RouteRepository _repository = new();

    [Fact]
    public void RouteRepository_Bakerloo_Routes_Correct()
    {
        var result = _repository.Lines[Guid.Parse("e6d7a23e-0f5f-4a90-a1c7-4e8e48c64823")];

        result.Name.Should().Be("Bakerloo");
        result.ValidRoutes.Count.Should().Be(1);

        var resultStations = result.ValidRoutes.First();
        resultStations.Stations.Count.Should().Be(25);


        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("a3c4de46-14f3-420c-8f55-c94e44f3b079"), Name = "Elephant & Castle" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("6be3e9ed-66af-4255-8910-d4b5857ba90a"), Name = "Harrow & Wealdstone" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("a3c4de46-14f3-420c-8f55-c94e44f3b079"), Name = "Elephant & Castle" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("432255d5-8b8c-4bea-a078-1aaf03152f9e"), Name = "Lambeth North" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("8cebdb43-8d17-49a2-a06b-1f3513091845"), Name = "Waterloo" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("ae58d763-b367-4b09-9f1d-3be50467f47f"), Name = "Embankment" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("2dfc6d93-0d29-4525-9d64-2bcaaffe873b"), Name = "Charing Cross" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("da1c4ff7-2831-48f0-a7a2-112a03c68d37"), Name = "Piccadilly Circus" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("246a422c-17d4-45ce-8cf9-0f36413d08e6"), Name = "Oxford Circus" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("60fb2105-6a25-4cc9-b7b7-5af01f30d66e"), Name = "Regent's Park" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("7d89b35f-9a87-49df-98ff-fd98f1f67235"), Name = "Baker Street" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("0bc90ea8-0436-437c-a81d-c5901c40d225"), Name = "Marylebone" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("afab4380-82ea-49ce-bf83-15bb3258110b"), Name = "Edgware Road" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("6a52e0a7-0801-4f1a-85bd-a34f70955052"), Name = "Paddington" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("61b0e4db-17b2-46d2-8c88-0289df8a31d7"), Name = "Warwick Avenue" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("b3e6363a-f4e5-4577-9103-e1cae1806f1c"), Name = "Maida Vale" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("aa1db58c-7da2-42d2-91da-3e38e92f4574"), Name = "Kilburn Park" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("07f797b2-82bc-44c4-babf-52ed8cae31f1"), Name = "Queen's Park" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("ac87fec7-bea8-4c87-b09d-4e2826803d0d"), Name = "Kensal Green" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("d2323699-fac3-412d-af16-c4cd9da671f2"), Name = "Willesden Junction" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("e4a9c350-d6e4-41db-8d2d-5f9512ccf0fa"), Name = "Harlesden" });
        resultStations.Stations[19].Should().BeEquivalentTo(new { Id = Guid.Parse("2ccab4c9-a72c-4ac2-8d23-b81c75c28771"), Name = "Stonebridge Park" });
        resultStations.Stations[20].Should().BeEquivalentTo(new { Id = Guid.Parse("73fdbc30-94f4-49e4-b29c-62d9a378d602"), Name = "Wembley Central" });
        resultStations.Stations[21].Should().BeEquivalentTo(new { Id = Guid.Parse("9ffe4518-80c1-4e1d-87d1-1d84f5a18dfd"), Name = "North Wembley" });
        resultStations.Stations[22].Should().BeEquivalentTo(new { Id = Guid.Parse("22202e09-4136-434c-b3b3-44748c24b936"), Name = "South Kenton" });
        resultStations.Stations[23].Should().BeEquivalentTo(new { Id = Guid.Parse("30a08758-735a-4539-ab0e-6070bc0c4fd5"), Name = "Kenton" });
        resultStations.Stations[24].Should().BeEquivalentTo(new { Id = Guid.Parse("6be3e9ed-66af-4255-8910-d4b5857ba90a"), Name = "Harrow & Wealdstone" });
    }

    [Fact]
    public void RouteRepository_Central_Routes_Correct()
    {
        var result = _repository.Lines[Guid.Parse("c7f7c41a-03d2-4a79-9e8e-b55b1b5a056e")];

        result.Name.Should().Be("Central");
        result.ValidRoutes.Count.Should().Be(5);

        var resultStations = result.ValidRoutes[0];
        resultStations.Stations.Count.Should().Be(32);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("2e67db42-0ae1-4406-9df5-666e8b64df28"), Name = "Ealing Broadway" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("def5a16c-7ef5-45d5-8f10-c6a700fb6144"), Name = "Epping" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("2e67db42-0ae1-4406-9df5-666e8b64df28"), Name = "Ealing Broadway" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("07adcb54-fbf0-46ce-a247-1685a3068d0b"), Name = "West Acton" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("67a0d88b-008e-4613-acc1-54a515e717d9"), Name = "North Acton" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("37302af0-85e6-4417-8409-61df83d3595b"), Name = "East Acton" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("dfe2f641-17ea-4ff6-bc45-d8fbc20ef057"), Name = "White City" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("4c4529fd-1b39-4a34-afb2-b0c815151012"), Name = "Shepherd's Bush" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("12d58547-1141-49a3-971b-3b15b886baef"), Name = "Holland Park" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("cd981628-5257-4fd1-a657-7168613eb50d"), Name = "Notting Hill Gate" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("200c6d17-4155-4113-99d2-b1f83482f76b"), Name = "Queensway" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("2dac0fb5-0b0c-492e-8199-431482324128"), Name = "Lancaster Gate" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("6371b2de-c503-40d1-ac8d-4f100c18ac5e"), Name = "Marble Arch" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("d2621069-fea8-4b31-8b56-16048f6b949d"), Name = "Bond Street" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("246a422c-17d4-45ce-8cf9-0f36413d08e6"), Name = "Oxford Circus" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("70f156b3-520a-46e7-a35d-3f603c6ab5b7"), Name = "Tottenham Court Road" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("92fef3de-c4db-47d2-ac5c-a188c6ab604c"), Name = "Holborn" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("44e87f5b-015d-42f8-a250-232e226de45b"), Name = "Chancery Lane" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("299580df-c896-486f-898d-c51f4a0bd0d2"), Name = "St.Paul's" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("aaedc653-e766-4d6b-87e2-4c87322971ef"), Name = "Bank" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("db101bcd-350e-4485-b875-7ac2c8c1b6cc"), Name = "Liverpool Street" });
        resultStations.Stations[19].Should().BeEquivalentTo(new { Id = Guid.Parse("b7a5ae67-882b-4509-8df9-4bae2ef1dd2a"), Name = "Bethnal Green" });
        resultStations.Stations[20].Should().BeEquivalentTo(new { Id = Guid.Parse("73bce1de-143f-4903-928a-c34ceb3db42e"), Name = "Mile End" });
        resultStations.Stations[21].Should().BeEquivalentTo(new { Id = Guid.Parse("b02ebcb8-83a4-48e1-85d8-6e3fb21fa058"), Name = "Stratford" });
        resultStations.Stations[22].Should().BeEquivalentTo(new { Id = Guid.Parse("2cd9c6bb-4c4f-4d09-a2ca-cd84ea273ecd"), Name = "Leyton" });
        resultStations.Stations[23].Should().BeEquivalentTo(new { Id = Guid.Parse("c1b799f3-b1d8-4bc6-adc3-28d809b8dd0b"), Name = "Leytonstone" });
        resultStations.Stations[24].Should().BeEquivalentTo(new { Id = Guid.Parse("baa03df3-8da7-434d-94ad-eda1defde099"), Name = "Snaresbrook" });
        resultStations.Stations[25].Should().BeEquivalentTo(new { Id = Guid.Parse("2a29dc10-8e19-410f-9567-3eea22066eda"), Name = "South Woodford" });
        resultStations.Stations[26].Should().BeEquivalentTo(new { Id = Guid.Parse("1dd5f2ad-13c9-49f9-93ac-5b1342a55aa8"), Name = "Woodford" });
        resultStations.Stations[27].Should().BeEquivalentTo(new { Id = Guid.Parse("2b8190bf-23fe-48f8-ad6b-4657c2ee59d4"), Name = "Buckhurst Hill" });
        resultStations.Stations[28].Should().BeEquivalentTo(new { Id = Guid.Parse("2bf9d5c4-1408-4968-bd42-c0bce7259001"), Name = "Loughton" });
        resultStations.Stations[29].Should().BeEquivalentTo(new { Id = Guid.Parse("fd19a884-54e4-42b0-b090-46f9928c9da4"), Name = "Debden" });
        resultStations.Stations[30].Should().BeEquivalentTo(new { Id = Guid.Parse("806a32c5-e466-4a6f-b949-4f7f531e92ee"), Name = "Theydon Bois" });
        resultStations.Stations[31].Should().BeEquivalentTo(new { Id = Guid.Parse("def5a16c-7ef5-45d5-8f10-c6a700fb6144"), Name = "Epping" });

        resultStations = result.ValidRoutes[1];
        resultStations.Stations.Count.Should().Be(31);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("2e67db42-0ae1-4406-9df5-666e8b64df28"), Name = "Ealing Broadway" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("aaa2565a-ba57-449b-b9b0-12bad4fb43a2"), Name = "Hainault" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("2e67db42-0ae1-4406-9df5-666e8b64df28"), Name = "Ealing Broadway" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("07adcb54-fbf0-46ce-a247-1685a3068d0b"), Name = "West Acton" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("67a0d88b-008e-4613-acc1-54a515e717d9"), Name = "North Acton" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("37302af0-85e6-4417-8409-61df83d3595b"), Name = "East Acton" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("dfe2f641-17ea-4ff6-bc45-d8fbc20ef057"), Name = "White City" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("4c4529fd-1b39-4a34-afb2-b0c815151012"), Name = "Shepherd's Bush" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("12d58547-1141-49a3-971b-3b15b886baef"), Name = "Holland Park" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("cd981628-5257-4fd1-a657-7168613eb50d"), Name = "Notting Hill Gate" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("200c6d17-4155-4113-99d2-b1f83482f76b"), Name = "Queensway" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("2dac0fb5-0b0c-492e-8199-431482324128"), Name = "Lancaster Gate" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("6371b2de-c503-40d1-ac8d-4f100c18ac5e"), Name = "Marble Arch" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("d2621069-fea8-4b31-8b56-16048f6b949d"), Name = "Bond Street" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("246a422c-17d4-45ce-8cf9-0f36413d08e6"), Name = "Oxford Circus" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("70f156b3-520a-46e7-a35d-3f603c6ab5b7"), Name = "Tottenham Court Road" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("92fef3de-c4db-47d2-ac5c-a188c6ab604c"), Name = "Holborn" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("44e87f5b-015d-42f8-a250-232e226de45b"), Name = "Chancery Lane" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("299580df-c896-486f-898d-c51f4a0bd0d2"), Name = "St.Paul's" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("aaedc653-e766-4d6b-87e2-4c87322971ef"), Name = "Bank" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("db101bcd-350e-4485-b875-7ac2c8c1b6cc"), Name = "Liverpool Street" });
        resultStations.Stations[19].Should().BeEquivalentTo(new { Id = Guid.Parse("b7a5ae67-882b-4509-8df9-4bae2ef1dd2a"), Name = "Bethnal Green" });
        resultStations.Stations[20].Should().BeEquivalentTo(new { Id = Guid.Parse("73bce1de-143f-4903-928a-c34ceb3db42e"), Name = "Mile End" });
        resultStations.Stations[21].Should().BeEquivalentTo(new { Id = Guid.Parse("b02ebcb8-83a4-48e1-85d8-6e3fb21fa058"), Name = "Stratford" });
        resultStations.Stations[22].Should().BeEquivalentTo(new { Id = Guid.Parse("2cd9c6bb-4c4f-4d09-a2ca-cd84ea273ecd"), Name = "Leyton" });
        resultStations.Stations[23].Should().BeEquivalentTo(new { Id = Guid.Parse("c1b799f3-b1d8-4bc6-adc3-28d809b8dd0b"), Name = "Leytonstone" });
        resultStations.Stations[24].Should().BeEquivalentTo(new { Id = Guid.Parse("a415070e-6e29-4269-bdb5-4f34bd344cec"), Name = "Wanstead" });
        resultStations.Stations[25].Should().BeEquivalentTo(new { Id = Guid.Parse("c28863e6-ac2e-42b8-9da2-03a5c470e1ac"), Name = "Redbridge" });
        resultStations.Stations[26].Should().BeEquivalentTo(new { Id = Guid.Parse("e4148baf-d38b-4eab-b4a1-32902831764e"), Name = "Gants Hill" });
        resultStations.Stations[27].Should().BeEquivalentTo(new { Id = Guid.Parse("22da7a99-62db-41de-9f01-a2e36fa3685a"), Name = "Newbury Park" });
        resultStations.Stations[28].Should().BeEquivalentTo(new { Id = Guid.Parse("4bcbe16a-5851-49ad-9d53-6cf2286392b4"), Name = "Barkingside" });
        resultStations.Stations[29].Should().BeEquivalentTo(new { Id = Guid.Parse("871272dd-7679-4657-b3e1-85d201e1d5b7"), Name = "Fairlop" });
        resultStations.Stations[30].Should().BeEquivalentTo(new { Id = Guid.Parse("aaa2565a-ba57-449b-b9b0-12bad4fb43a2"), Name = "Hainault" });

        resultStations = result.ValidRoutes[2];
        resultStations.Stations.Count.Should().Be(36);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("b2b51366-0448-4a6a-84c8-53e5cfabcb4c"), Name = "West Ruislip" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("def5a16c-7ef5-45d5-8f10-c6a700fb6144"), Name = "Epping" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("b2b51366-0448-4a6a-84c8-53e5cfabcb4c"), Name = "West Ruislip" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("62bd7557-089b-4099-b322-956956ed9721"), Name = "Ruislip Gardens" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("5c20d5d5-3ddc-4200-8220-505df6100246"), Name = "South Ruislip" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("e34ed4a9-d422-4c9d-92d9-51ffcc2a6a16"), Name = "Northolt" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("404d80d5-2e34-4f3b-98a4-2198398d0232"), Name = "Greenford" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("b311ea03-3802-4cf1-aaa6-ef140e058e53"), Name = "Perivale" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("70b70903-3f3a-4910-8dd4-6dc8c128e8ad"), Name = "Hanger Lane" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("67a0d88b-008e-4613-acc1-54a515e717d9"), Name = "North Acton" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("37302af0-85e6-4417-8409-61df83d3595b"), Name = "East Acton" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("dfe2f641-17ea-4ff6-bc45-d8fbc20ef057"), Name = "White City" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("4c4529fd-1b39-4a34-afb2-b0c815151012"), Name = "Shepherd's Bush" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("12d58547-1141-49a3-971b-3b15b886baef"), Name = "Holland Park" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("cd981628-5257-4fd1-a657-7168613eb50d"), Name = "Notting Hill Gate" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("200c6d17-4155-4113-99d2-b1f83482f76b"), Name = "Queensway" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("2dac0fb5-0b0c-492e-8199-431482324128"), Name = "Lancaster Gate" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("6371b2de-c503-40d1-ac8d-4f100c18ac5e"), Name = "Marble Arch" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("d2621069-fea8-4b31-8b56-16048f6b949d"), Name = "Bond Street" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("246a422c-17d4-45ce-8cf9-0f36413d08e6"), Name = "Oxford Circus" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("70f156b3-520a-46e7-a35d-3f603c6ab5b7"), Name = "Tottenham Court Road" });
        resultStations.Stations[19].Should().BeEquivalentTo(new { Id = Guid.Parse("92fef3de-c4db-47d2-ac5c-a188c6ab604c"), Name = "Holborn" });
        resultStations.Stations[20].Should().BeEquivalentTo(new { Id = Guid.Parse("44e87f5b-015d-42f8-a250-232e226de45b"), Name = "Chancery Lane" });
        resultStations.Stations[21].Should().BeEquivalentTo(new { Id = Guid.Parse("aaedc653-e766-4d6b-87e2-4c87322971ef"), Name = "Bank" });
        resultStations.Stations[22].Should().BeEquivalentTo(new { Id = Guid.Parse("db101bcd-350e-4485-b875-7ac2c8c1b6cc"), Name = "Liverpool Street" });
        resultStations.Stations[23].Should().BeEquivalentTo(new { Id = Guid.Parse("b7a5ae67-882b-4509-8df9-4bae2ef1dd2a"), Name = "Bethnal Green" });
        resultStations.Stations[24].Should().BeEquivalentTo(new { Id = Guid.Parse("73bce1de-143f-4903-928a-c34ceb3db42e"), Name = "Mile End" });
        resultStations.Stations[25].Should().BeEquivalentTo(new { Id = Guid.Parse("b02ebcb8-83a4-48e1-85d8-6e3fb21fa058"), Name = "Stratford" });
        resultStations.Stations[26].Should().BeEquivalentTo(new { Id = Guid.Parse("2cd9c6bb-4c4f-4d09-a2ca-cd84ea273ecd"), Name = "Leyton" });
        resultStations.Stations[27].Should().BeEquivalentTo(new { Id = Guid.Parse("c1b799f3-b1d8-4bc6-adc3-28d809b8dd0b"), Name = "Leytonstone" });
        resultStations.Stations[28].Should().BeEquivalentTo(new { Id = Guid.Parse("baa03df3-8da7-434d-94ad-eda1defde099"), Name = "Snaresbrook" });
        resultStations.Stations[29].Should().BeEquivalentTo(new { Id = Guid.Parse("2a29dc10-8e19-410f-9567-3eea22066eda"), Name = "South Woodford" });
        resultStations.Stations[30].Should().BeEquivalentTo(new { Id = Guid.Parse("1dd5f2ad-13c9-49f9-93ac-5b1342a55aa8"), Name = "Woodford" });
        resultStations.Stations[31].Should().BeEquivalentTo(new { Id = Guid.Parse("2b8190bf-23fe-48f8-ad6b-4657c2ee59d4"), Name = "Buckhurst Hill" });
        resultStations.Stations[32].Should().BeEquivalentTo(new { Id = Guid.Parse("2bf9d5c4-1408-4968-bd42-c0bce7259001"), Name = "Loughton" });
        resultStations.Stations[33].Should().BeEquivalentTo(new { Id = Guid.Parse("fd19a884-54e4-42b0-b090-46f9928c9da4"), Name = "Debden" });
        resultStations.Stations[34].Should().BeEquivalentTo(new { Id = Guid.Parse("806a32c5-e466-4a6f-b949-4f7f531e92ee"), Name = "Theydon Bois" });
        resultStations.Stations[35].Should().BeEquivalentTo(new { Id = Guid.Parse("def5a16c-7ef5-45d5-8f10-c6a700fb6144"), Name = "Epping" });

        resultStations = result.ValidRoutes[3];
        resultStations.Stations.Count.Should().Be(36);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("b2b51366-0448-4a6a-84c8-53e5cfabcb4c"), Name = "West Ruislip" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("aaa2565a-ba57-449b-b9b0-12bad4fb43a2"), Name = "Hainault" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("b2b51366-0448-4a6a-84c8-53e5cfabcb4c"), Name = "West Ruislip" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("62bd7557-089b-4099-b322-956956ed9721"), Name = "Ruislip Gardens" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("5c20d5d5-3ddc-4200-8220-505df6100246"), Name = "South Ruislip" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("e34ed4a9-d422-4c9d-92d9-51ffcc2a6a16"), Name = "Northolt" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("404d80d5-2e34-4f3b-98a4-2198398d0232"), Name = "Greenford" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("b311ea03-3802-4cf1-aaa6-ef140e058e53"), Name = "Perivale" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("70b70903-3f3a-4910-8dd4-6dc8c128e8ad"), Name = "Hanger Lane" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("67a0d88b-008e-4613-acc1-54a515e717d9"), Name = "North Acton" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("37302af0-85e6-4417-8409-61df83d3595b"), Name = "East Acton" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("dfe2f641-17ea-4ff6-bc45-d8fbc20ef057"), Name = "White City" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("4c4529fd-1b39-4a34-afb2-b0c815151012"), Name = "Shepherd's Bush" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("12d58547-1141-49a3-971b-3b15b886baef"), Name = "Holland Park" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("cd981628-5257-4fd1-a657-7168613eb50d"), Name = "Notting Hill Gate" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("200c6d17-4155-4113-99d2-b1f83482f76b"), Name = "Queensway" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("2dac0fb5-0b0c-492e-8199-431482324128"), Name = "Lancaster Gate" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("6371b2de-c503-40d1-ac8d-4f100c18ac5e"), Name = "Marble Arch" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("d2621069-fea8-4b31-8b56-16048f6b949d"), Name = "Bond Street" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("246a422c-17d4-45ce-8cf9-0f36413d08e6"), Name = "Oxford Circus" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("70f156b3-520a-46e7-a35d-3f603c6ab5b7"), Name = "Tottenham Court Road" });
        resultStations.Stations[19].Should().BeEquivalentTo(new { Id = Guid.Parse("92fef3de-c4db-47d2-ac5c-a188c6ab604c"), Name = "Holborn" });
        resultStations.Stations[20].Should().BeEquivalentTo(new { Id = Guid.Parse("44e87f5b-015d-42f8-a250-232e226de45b"), Name = "Chancery Lane" });
        resultStations.Stations[21].Should().BeEquivalentTo(new { Id = Guid.Parse("299580df-c896-486f-898d-c51f4a0bd0d2"), Name = "St.Paul's" });
        resultStations.Stations[22].Should().BeEquivalentTo(new { Id = Guid.Parse("aaedc653-e766-4d6b-87e2-4c87322971ef"), Name = "Bank" });
        resultStations.Stations[23].Should().BeEquivalentTo(new { Id = Guid.Parse("db101bcd-350e-4485-b875-7ac2c8c1b6cc"), Name = "Liverpool Street" });
        resultStations.Stations[24].Should().BeEquivalentTo(new { Id = Guid.Parse("b7a5ae67-882b-4509-8df9-4bae2ef1dd2a"), Name = "Bethnal Green" });
        resultStations.Stations[25].Should().BeEquivalentTo(new { Id = Guid.Parse("73bce1de-143f-4903-928a-c34ceb3db42e"), Name = "Mile End" });
        resultStations.Stations[26].Should().BeEquivalentTo(new { Id = Guid.Parse("b02ebcb8-83a4-48e1-85d8-6e3fb21fa058"), Name = "Stratford" });
        resultStations.Stations[27].Should().BeEquivalentTo(new { Id = Guid.Parse("2cd9c6bb-4c4f-4d09-a2ca-cd84ea273ecd"), Name = "Leyton" });
        resultStations.Stations[28].Should().BeEquivalentTo(new { Id = Guid.Parse("c1b799f3-b1d8-4bc6-adc3-28d809b8dd0b"), Name = "Leytonstone" });
        resultStations.Stations[29].Should().BeEquivalentTo(new { Id = Guid.Parse("baa03df3-8da7-434d-94ad-eda1defde099"), Name = "Snaresbrook" });
        resultStations.Stations[30].Should().BeEquivalentTo(new { Id = Guid.Parse("2a29dc10-8e19-410f-9567-3eea22066eda"), Name = "South Woodford" });
        resultStations.Stations[31].Should().BeEquivalentTo(new { Id = Guid.Parse("1dd5f2ad-13c9-49f9-93ac-5b1342a55aa8"), Name = "Woodford" });
        resultStations.Stations[32].Should().BeEquivalentTo(new { Id = Guid.Parse("76849db1-b8d9-484e-bf74-c7b8b405a52e"), Name = "Roding Valley" });
        resultStations.Stations[33].Should().BeEquivalentTo(new { Id = Guid.Parse("33c0fe6d-8685-4a67-a860-6af4ed07c5f4"), Name = "Chigwell" });
        resultStations.Stations[34].Should().BeEquivalentTo(new { Id = Guid.Parse("16ee0fe4-4e89-4639-88b6-3940b26cd2bb"), Name = "Grange Hill" });
        resultStations.Stations[35].Should().BeEquivalentTo(new { Id = Guid.Parse("aaa2565a-ba57-449b-b9b0-12bad4fb43a2"), Name = "Hainault" });

        resultStations = result.ValidRoutes[4];
        resultStations.Stations.Count.Should().Be(35);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("b2b51366-0448-4a6a-84c8-53e5cfabcb4c"), Name = "West Ruislip" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("aaa2565a-ba57-449b-b9b0-12bad4fb43a2"), Name = "Hainault" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("b2b51366-0448-4a6a-84c8-53e5cfabcb4c"), Name = "West Ruislip" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("62bd7557-089b-4099-b322-956956ed9721"), Name = "Ruislip Gardens" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("5c20d5d5-3ddc-4200-8220-505df6100246"), Name = "South Ruislip" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("e34ed4a9-d422-4c9d-92d9-51ffcc2a6a16"), Name = "Northolt" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("404d80d5-2e34-4f3b-98a4-2198398d0232"), Name = "Greenford" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("b311ea03-3802-4cf1-aaa6-ef140e058e53"), Name = "Perivale" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("70b70903-3f3a-4910-8dd4-6dc8c128e8ad"), Name = "Hanger Lane" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("67a0d88b-008e-4613-acc1-54a515e717d9"), Name = "North Acton" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("37302af0-85e6-4417-8409-61df83d3595b"), Name = "East Acton" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("dfe2f641-17ea-4ff6-bc45-d8fbc20ef057"), Name = "White City" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("4c4529fd-1b39-4a34-afb2-b0c815151012"), Name = "Shepherd's Bush" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("12d58547-1141-49a3-971b-3b15b886baef"), Name = "Holland Park" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("cd981628-5257-4fd1-a657-7168613eb50d"), Name = "Notting Hill Gate" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("200c6d17-4155-4113-99d2-b1f83482f76b"), Name = "Queensway" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("2dac0fb5-0b0c-492e-8199-431482324128"), Name = "Lancaster Gate" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("6371b2de-c503-40d1-ac8d-4f100c18ac5e"), Name = "Marble Arch" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("d2621069-fea8-4b31-8b56-16048f6b949d"), Name = "Bond Street" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("246a422c-17d4-45ce-8cf9-0f36413d08e6"), Name = "Oxford Circus" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("70f156b3-520a-46e7-a35d-3f603c6ab5b7"), Name = "Tottenham Court Road" });
        resultStations.Stations[19].Should().BeEquivalentTo(new { Id = Guid.Parse("92fef3de-c4db-47d2-ac5c-a188c6ab604c"), Name = "Holborn" });
        resultStations.Stations[20].Should().BeEquivalentTo(new { Id = Guid.Parse("44e87f5b-015d-42f8-a250-232e226de45b"), Name = "Chancery Lane" });
        resultStations.Stations[21].Should().BeEquivalentTo(new { Id = Guid.Parse("aaedc653-e766-4d6b-87e2-4c87322971ef"), Name = "Bank" });
        resultStations.Stations[22].Should().BeEquivalentTo(new { Id = Guid.Parse("db101bcd-350e-4485-b875-7ac2c8c1b6cc"), Name = "Liverpool Street" });
        resultStations.Stations[23].Should().BeEquivalentTo(new { Id = Guid.Parse("b7a5ae67-882b-4509-8df9-4bae2ef1dd2a"), Name = "Bethnal Green" });
        resultStations.Stations[24].Should().BeEquivalentTo(new { Id = Guid.Parse("73bce1de-143f-4903-928a-c34ceb3db42e"), Name = "Mile End" });
        resultStations.Stations[25].Should().BeEquivalentTo(new { Id = Guid.Parse("b02ebcb8-83a4-48e1-85d8-6e3fb21fa058"), Name = "Stratford" });
        resultStations.Stations[26].Should().BeEquivalentTo(new { Id = Guid.Parse("2cd9c6bb-4c4f-4d09-a2ca-cd84ea273ecd"), Name = "Leyton" });
        resultStations.Stations[27].Should().BeEquivalentTo(new { Id = Guid.Parse("c1b799f3-b1d8-4bc6-adc3-28d809b8dd0b"), Name = "Leytonstone" });
        resultStations.Stations[28].Should().BeEquivalentTo(new { Id = Guid.Parse("a415070e-6e29-4269-bdb5-4f34bd344cec"), Name = "Wanstead" });
        resultStations.Stations[29].Should().BeEquivalentTo(new { Id = Guid.Parse("c28863e6-ac2e-42b8-9da2-03a5c470e1ac"), Name = "Redbridge" });
        resultStations.Stations[30].Should().BeEquivalentTo(new { Id = Guid.Parse("e4148baf-d38b-4eab-b4a1-32902831764e"), Name = "Gants Hill" });
        resultStations.Stations[31].Should().BeEquivalentTo(new { Id = Guid.Parse("22da7a99-62db-41de-9f01-a2e36fa3685a"), Name = "Newbury Park" });
        resultStations.Stations[32].Should().BeEquivalentTo(new { Id = Guid.Parse("4bcbe16a-5851-49ad-9d53-6cf2286392b4"), Name = "Barkingside" });
        resultStations.Stations[33].Should().BeEquivalentTo(new { Id = Guid.Parse("871272dd-7679-4657-b3e1-85d201e1d5b7"), Name = "Fairlop" });
        resultStations.Stations[34].Should().BeEquivalentTo(new { Id = Guid.Parse("aaa2565a-ba57-449b-b9b0-12bad4fb43a2"), Name = "Hainault" });
    }

    [Fact]
    public void RouteRepository_Circle_Routes_Correct()
    {
        var result = _repository.Lines[Guid.Parse("5e8c1a94-5f0c-4d4d-8c4b-07bba9f5eb54")];

        result.Name.Should().Be("Circle");
        result.ValidRoutes.Count.Should().Be(1);

        var resultStations = result.ValidRoutes[0];
        resultStations.Stations.Count.Should().Be(37);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("bdf7bc22-9e0b-4bfc-8abe-8130f5c462c8"), Name = "Hammersmith" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("afab4380-82ea-49ce-bf83-15bb3258110b"), Name = "Edgware Road" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("bdf7bc22-9e0b-4bfc-8abe-8130f5c462c8"), Name = "Hammersmith" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("67848e8e-cbb2-41d9-85f8-a992435638e7"), Name = "Goldhawk Road" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("2b981aba-c5c5-4d34-93a0-e27bc226cb9e"), Name = "Shepherd's Bush Market" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("3a6ffedf-1039-4206-8739-b922ead8fdfc"), Name = "Wood Lane" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("810e52d6-505d-4440-8ce7-6269184998aa"), Name = "Latimer Road" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("0bb0e1a9-8195-4ea1-95d7-98cb89f72418"), Name = "Ladbroke Grove" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("b72c56eb-b3ec-4dba-b489-fe7418a0990c"), Name = "Westbourne Park" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("d02b6015-3e16-4f90-9421-1c5395595ec9"), Name = "Royal Oak" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("6a52e0a7-0801-4f1a-85bd-a34f70955052"), Name = "Paddington" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("afab4380-82ea-49ce-bf83-15bb3258110b"), Name = "Edgware Road" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("7d89b35f-9a87-49df-98ff-fd98f1f67235"), Name = "Baker Street" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("bd2ef776-2cea-49ad-80c4-a7e6587cc67b"), Name = "Great Portland Street" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("0ec876ed-6a2e-48e3-b8b0-6eb2ab13fc12"), Name = "Euston Square" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("5cf18e37-17e0-46c0-9177-6a5951df26b8"), Name = "King's Cross St.Pancras" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("bf6a3d1b-2af9-4b1c-b696-fc6f8c8cecde"), Name = "Farringdon" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("10972919-8db8-4a6c-aadf-ace0e43b5d8c"), Name = "Barbican" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("9343886e-5b86-4bbc-96bf-2fefe6240060"), Name = "Moorgate" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("db101bcd-350e-4485-b875-7ac2c8c1b6cc"), Name = "Liverpool Street" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("2c5fbeab-ba64-4c8b-b21e-2336cdad37a5"), Name = "Aldgate" });
        resultStations.Stations[19].Should().BeEquivalentTo(new { Id = Guid.Parse("e82d6990-68ae-43e2-a353-71135b2075e3"), Name = "Tower Hill" });
        resultStations.Stations[20].Should().BeEquivalentTo(new { Id = Guid.Parse("5514e4ab-7cd9-4e07-8fe6-57ecf30b69ea"), Name = "Monument" });
        resultStations.Stations[21].Should().BeEquivalentTo(new { Id = Guid.Parse("65d97039-79a9-41e2-9351-21ca5d866b14"), Name = "Cannon Street" });
        resultStations.Stations[22].Should().BeEquivalentTo(new { Id = Guid.Parse("4e490fff-0a4c-459b-9a52-c63d4bb3ceba"), Name = "Mansion House" });
        resultStations.Stations[23].Should().BeEquivalentTo(new { Id = Guid.Parse("dba0d82b-4618-4fb4-9b79-cf2a22c9d9c1"), Name = "Blackfriars" });
        resultStations.Stations[24].Should().BeEquivalentTo(new { Id = Guid.Parse("a277af62-5304-4154-86cb-344933d8c7f3"), Name = "Temple" });
        resultStations.Stations[25].Should().BeEquivalentTo(new { Id = Guid.Parse("ae58d763-b367-4b09-9f1d-3be50467f47f"), Name = "Embankment" });
        resultStations.Stations[26].Should().BeEquivalentTo(new { Id = Guid.Parse("d58a55c8-bd8d-45f7-a7a2-c0bd0096afca"), Name = "Westminster" });
        resultStations.Stations[27].Should().BeEquivalentTo(new { Id = Guid.Parse("3fdb5fa9-f04e-4348-97e9-a4e9d9948ca4"), Name = "St.James's Park" });
        resultStations.Stations[28].Should().BeEquivalentTo(new { Id = Guid.Parse("9d0f8c47-3708-489a-99b3-1a6d960341e6"), Name = "Victoria" });
        resultStations.Stations[29].Should().BeEquivalentTo(new { Id = Guid.Parse("ab3c57e2-61cb-470d-8644-7926e2896c50"), Name = "Sloane Square" });
        resultStations.Stations[30].Should().BeEquivalentTo(new { Id = Guid.Parse("4afcf7aa-f5ee-499f-9484-38e0b0c0af0b"), Name = "South Kensington" });
        resultStations.Stations[31].Should().BeEquivalentTo(new { Id = Guid.Parse("94b2e2cc-4150-4ab0-b387-21e2a13960e1"), Name = "Gloucester Road" });
        resultStations.Stations[32].Should().BeEquivalentTo(new { Id = Guid.Parse("d60ce047-7589-4458-96b7-300ae3737941"), Name = "High Street Kensington" });
        resultStations.Stations[33].Should().BeEquivalentTo(new { Id = Guid.Parse("cd981628-5257-4fd1-a657-7168613eb50d"), Name = "Notting Hill Gate" });
        resultStations.Stations[34].Should().BeEquivalentTo(new { Id = Guid.Parse("cb25b18f-4fee-4b93-a63e-79921a6cfc62"), Name = "Bayswater" });
        resultStations.Stations[35].Should().BeEquivalentTo(new { Id = Guid.Parse("6a52e0a7-0801-4f1a-85bd-a34f70955052"), Name = "Paddington" });
        resultStations.Stations[36].Should().BeEquivalentTo(new { Id = Guid.Parse("afab4380-82ea-49ce-bf83-15bb3258110b"), Name = "Edgware Road" });
    }

    [Fact]
    public void RouteRepository_District_Routes_Correct()
    {
        var result = _repository.Lines[Guid.Parse("8c3a4d59-f2e0-46a8-9f56-ec27eaffded9")];

        result.Name.Should().Be("District");
        result.ValidRoutes.Count.Should().Be(7);

        var resultStations = result.ValidRoutes[0];
        resultStations.Stations.Count.Should().Be(43);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("2e67db42-0ae1-4406-9df5-666e8b64df28"), Name = "Ealing Broadway" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("2a423032-a8ab-40dc-8b3b-44085632d1a9"), Name = "Upminster" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("2e67db42-0ae1-4406-9df5-666e8b64df28"), Name = "Ealing Broadway" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("e4c13d97-dc83-491e-8e2b-c80af1c21064"), Name = "Ealing Common" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("82a3d734-abf8-43f1-853d-a69f938c20b1"), Name = "Acton Town" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("70e49090-1f25-470a-87a3-a4682ff174b1"), Name = "Chiswick Park" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("612f4d89-3086-4e1b-9771-d0e748718eb8"), Name = "Turnham Green" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("07fbc482-414a-4f9c-a30f-c4cf9f752e38"), Name = "Stamford Brook" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("20f6b18f-59d0-4139-93df-2dcc022eec77"), Name = "Ravenscourt Park" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("bdf7bc22-9e0b-4bfc-8abe-8130f5c462c8"), Name = "Hammersmith" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("6ea41ee3-5e14-4830-af0a-02d2bd9bcf98"), Name = "Barons Court" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("407f81fa-3a8f-4af8-a707-6042ae296236"), Name = "West Kensington" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("03c1cead-6d76-40f7-b67d-0eecef00220b"), Name = "Earl's Court" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("94b2e2cc-4150-4ab0-b387-21e2a13960e1"), Name = "Gloucester Road" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("4afcf7aa-f5ee-499f-9484-38e0b0c0af0b"), Name = "South Kensington" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("ab3c57e2-61cb-470d-8644-7926e2896c50"), Name = "Sloane Square" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("9d0f8c47-3708-489a-99b3-1a6d960341e6"), Name = "Victoria" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("3fdb5fa9-f04e-4348-97e9-a4e9d9948ca4"), Name = "St.James's Park" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("d58a55c8-bd8d-45f7-a7a2-c0bd0096afca"), Name = "Westminster" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("ae58d763-b367-4b09-9f1d-3be50467f47f"), Name = "Embankment" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("a277af62-5304-4154-86cb-344933d8c7f3"), Name = "Temple" });
        resultStations.Stations[19].Should().BeEquivalentTo(new { Id = Guid.Parse("dba0d82b-4618-4fb4-9b79-cf2a22c9d9c1"), Name = "Blackfriars" });
        resultStations.Stations[20].Should().BeEquivalentTo(new { Id = Guid.Parse("4e490fff-0a4c-459b-9a52-c63d4bb3ceba"), Name = "Mansion House" });
        resultStations.Stations[21].Should().BeEquivalentTo(new { Id = Guid.Parse("65d97039-79a9-41e2-9351-21ca5d866b14"), Name = "Cannon Street" });
        resultStations.Stations[22].Should().BeEquivalentTo(new { Id = Guid.Parse("5514e4ab-7cd9-4e07-8fe6-57ecf30b69ea"), Name = "Monument" });
        resultStations.Stations[23].Should().BeEquivalentTo(new { Id = Guid.Parse("e82d6990-68ae-43e2-a353-71135b2075e3"), Name = "Tower Hill" });
        resultStations.Stations[24].Should().BeEquivalentTo(new { Id = Guid.Parse("e82e025a-1362-4f11-9da9-9625cd8ac71d"), Name = "Aldgate East" });
        resultStations.Stations[25].Should().BeEquivalentTo(new { Id = Guid.Parse("9787df93-f917-4890-9e0b-8b36e795bf9b"), Name = "Whitechapel" });
        resultStations.Stations[26].Should().BeEquivalentTo(new { Id = Guid.Parse("4bb7447a-4182-4ba7-996d-07a2bcc119d1"), Name = "Stepney Green" });
        resultStations.Stations[27].Should().BeEquivalentTo(new { Id = Guid.Parse("73bce1de-143f-4903-928a-c34ceb3db42e"), Name = "Mile End" });
        resultStations.Stations[28].Should().BeEquivalentTo(new { Id = Guid.Parse("3db408d6-248a-4ef7-8486-203e87cc408a"), Name = "Bow Road" });
        resultStations.Stations[29].Should().BeEquivalentTo(new { Id = Guid.Parse("a391396c-6921-4202-ace2-2d5033bfac1f"), Name = "Bromley By Bow" });
        resultStations.Stations[30].Should().BeEquivalentTo(new { Id = Guid.Parse("968bc258-138c-45cf-83c0-599705285d25"), Name = "West Ham" });
        resultStations.Stations[31].Should().BeEquivalentTo(new { Id = Guid.Parse("b9b356ad-31ac-41da-998d-72dcca4c621f"), Name = "Plaistow" });
        resultStations.Stations[32].Should().BeEquivalentTo(new { Id = Guid.Parse("11d96cde-06ba-4160-8200-cef94ccdd6d8"), Name = "Upton Park" });
        resultStations.Stations[33].Should().BeEquivalentTo(new { Id = Guid.Parse("66d453d7-94b8-4969-abaf-ba04036f850e"), Name = "East Ham" });
        resultStations.Stations[34].Should().BeEquivalentTo(new { Id = Guid.Parse("1c5faedb-30a6-4957-a8f7-6cdc702f4f68"), Name = "Barking" });
        resultStations.Stations[35].Should().BeEquivalentTo(new { Id = Guid.Parse("77f3aa5d-6a53-451a-9c65-3531ae5a7fc8"), Name = "Upney" });
        resultStations.Stations[36].Should().BeEquivalentTo(new { Id = Guid.Parse("f2ae9bb5-ccc5-4a4f-ab6b-94cff25f9e6c"), Name = "Becontree" });
        resultStations.Stations[37].Should().BeEquivalentTo(new { Id = Guid.Parse("d36e54a7-6651-49e8-ae5a-79d0700880f1"), Name = "Dagenham Heathway" });
        resultStations.Stations[38].Should().BeEquivalentTo(new { Id = Guid.Parse("50a61507-b3d8-4c0d-ac2d-916197e31392"), Name = "Dagenham East" });
        resultStations.Stations[39].Should().BeEquivalentTo(new { Id = Guid.Parse("e4b6eac7-93a1-4d02-a2bc-defa7802facf"), Name = "Elm Park" });
        resultStations.Stations[40].Should().BeEquivalentTo(new { Id = Guid.Parse("7dadbd81-a249-4ec5-b681-b99218f139fd"), Name = "Hornchurch" });
        resultStations.Stations[41].Should().BeEquivalentTo(new { Id = Guid.Parse("acf5008c-13f0-4439-91df-a45ab9bfbbd0"), Name = "Upminster Bridge" });
        resultStations.Stations[42].Should().BeEquivalentTo(new { Id = Guid.Parse("2a423032-a8ab-40dc-8b3b-44085632d1a9"), Name = "Upminster" });

        resultStations = result.ValidRoutes[1];
        resultStations.Stations.Count.Should().Be(16);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("2e67db42-0ae1-4406-9df5-666e8b64df28"), Name = "Ealing Broadway" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("afab4380-82ea-49ce-bf83-15bb3258110b"), Name = "Edgware Road" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("2e67db42-0ae1-4406-9df5-666e8b64df28"), Name = "Ealing Broadway" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("e4c13d97-dc83-491e-8e2b-c80af1c21064"), Name = "Ealing Common" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("82a3d734-abf8-43f1-853d-a69f938c20b1"), Name = "Acton Town" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("70e49090-1f25-470a-87a3-a4682ff174b1"), Name = "Chiswick Park" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("612f4d89-3086-4e1b-9771-d0e748718eb8"), Name = "Turnham Green" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("07fbc482-414a-4f9c-a30f-c4cf9f752e38"), Name = "Stamford Brook" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("20f6b18f-59d0-4139-93df-2dcc022eec77"), Name = "Ravenscourt Park" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("bdf7bc22-9e0b-4bfc-8abe-8130f5c462c8"), Name = "Hammersmith" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("6ea41ee3-5e14-4830-af0a-02d2bd9bcf98"), Name = "Barons Court" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("407f81fa-3a8f-4af8-a707-6042ae296236"), Name = "West Kensington" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("03c1cead-6d76-40f7-b67d-0eecef00220b"), Name = "Earl's Court" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("d60ce047-7589-4458-96b7-300ae3737941"), Name = "High Street Kensington" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("cd981628-5257-4fd1-a657-7168613eb50d"), Name = "Notting Hill Gate" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("cb25b18f-4fee-4b93-a63e-79921a6cfc62"), Name = "Bayswater" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("6a52e0a7-0801-4f1a-85bd-a34f70955052"), Name = "Paddington" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("afab4380-82ea-49ce-bf83-15bb3258110b"), Name = "Edgware Road" });

        resultStations = result.ValidRoutes[2];
        resultStations.Stations.Count.Should().Be(7);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("b9a55e52-dbdc-4542-9981-8afe9e9709fe"), Name = "Kensington (Olympia)" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("afab4380-82ea-49ce-bf83-15bb3258110b"), Name = "Edgware Road" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("b9a55e52-dbdc-4542-9981-8afe9e9709fe"), Name = "Kensington (Olympia)" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("03c1cead-6d76-40f7-b67d-0eecef00220b"), Name = "Earl's Court" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("d60ce047-7589-4458-96b7-300ae3737941"), Name = "High Street Kensington" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("cd981628-5257-4fd1-a657-7168613eb50d"), Name = "Notting Hill Gate" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("cb25b18f-4fee-4b93-a63e-79921a6cfc62"), Name = "Bayswater" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("6a52e0a7-0801-4f1a-85bd-a34f70955052"), Name = "Paddington" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("afab4380-82ea-49ce-bf83-15bb3258110b"), Name = "Edgware Road" });

        resultStations = result.ValidRoutes[3];
        resultStations.Stations.Count.Should().Be(42);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("10c300fa-acc5-4701-8a3d-ba27a3327696"), Name = "Richmond" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("2a423032-a8ab-40dc-8b3b-44085632d1a9"), Name = "Upminster" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("10c300fa-acc5-4701-8a3d-ba27a3327696"), Name = "Richmond" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("92a0ee87-b829-4669-9482-fb8620455b4b"), Name = "Kew Gardens" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("4a1b2adb-a70e-42d1-b7e7-a2845ca98e7c"), Name = "Gunnersbury" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("612f4d89-3086-4e1b-9771-d0e748718eb8"), Name = "Turnham Green" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("07fbc482-414a-4f9c-a30f-c4cf9f752e38"), Name = "Stamford Brook" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("20f6b18f-59d0-4139-93df-2dcc022eec77"), Name = "Ravenscourt Park" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("bdf7bc22-9e0b-4bfc-8abe-8130f5c462c8"), Name = "Hammersmith" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("6ea41ee3-5e14-4830-af0a-02d2bd9bcf98"), Name = "Barons Court" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("407f81fa-3a8f-4af8-a707-6042ae296236"), Name = "West Kensington" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("03c1cead-6d76-40f7-b67d-0eecef00220b"), Name = "Earl's Court" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("94b2e2cc-4150-4ab0-b387-21e2a13960e1"), Name = "Gloucester Road" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("4afcf7aa-f5ee-499f-9484-38e0b0c0af0b"), Name = "South Kensington" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("ab3c57e2-61cb-470d-8644-7926e2896c50"), Name = "Sloane Square" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("9d0f8c47-3708-489a-99b3-1a6d960341e6"), Name = "Victoria" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("3fdb5fa9-f04e-4348-97e9-a4e9d9948ca4"), Name = "St.James's Park" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("d58a55c8-bd8d-45f7-a7a2-c0bd0096afca"), Name = "Westminster" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("ae58d763-b367-4b09-9f1d-3be50467f47f"), Name = "Embankment" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("a277af62-5304-4154-86cb-344933d8c7f3"), Name = "Temple" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("dba0d82b-4618-4fb4-9b79-cf2a22c9d9c1"), Name = "Blackfriars" });
        resultStations.Stations[19].Should().BeEquivalentTo(new { Id = Guid.Parse("4e490fff-0a4c-459b-9a52-c63d4bb3ceba"), Name = "Mansion House" });
        resultStations.Stations[20].Should().BeEquivalentTo(new { Id = Guid.Parse("65d97039-79a9-41e2-9351-21ca5d866b14"), Name = "Cannon Street" });
        resultStations.Stations[21].Should().BeEquivalentTo(new { Id = Guid.Parse("5514e4ab-7cd9-4e07-8fe6-57ecf30b69ea"), Name = "Monument" });
        resultStations.Stations[22].Should().BeEquivalentTo(new { Id = Guid.Parse("e82d6990-68ae-43e2-a353-71135b2075e3"), Name = "Tower Hill" });
        resultStations.Stations[23].Should().BeEquivalentTo(new { Id = Guid.Parse("e82e025a-1362-4f11-9da9-9625cd8ac71d"), Name = "Aldgate East" });
        resultStations.Stations[24].Should().BeEquivalentTo(new { Id = Guid.Parse("9787df93-f917-4890-9e0b-8b36e795bf9b"), Name = "Whitechapel" });
        resultStations.Stations[25].Should().BeEquivalentTo(new { Id = Guid.Parse("4bb7447a-4182-4ba7-996d-07a2bcc119d1"), Name = "Stepney Green" });
        resultStations.Stations[26].Should().BeEquivalentTo(new { Id = Guid.Parse("73bce1de-143f-4903-928a-c34ceb3db42e"), Name = "Mile End" });
        resultStations.Stations[27].Should().BeEquivalentTo(new { Id = Guid.Parse("3db408d6-248a-4ef7-8486-203e87cc408a"), Name = "Bow Road" });
        resultStations.Stations[28].Should().BeEquivalentTo(new { Id = Guid.Parse("a391396c-6921-4202-ace2-2d5033bfac1f"), Name = "Bromley By Bow" });
        resultStations.Stations[29].Should().BeEquivalentTo(new { Id = Guid.Parse("968bc258-138c-45cf-83c0-599705285d25"), Name = "West Ham" });
        resultStations.Stations[30].Should().BeEquivalentTo(new { Id = Guid.Parse("b9b356ad-31ac-41da-998d-72dcca4c621f"), Name = "Plaistow" });
        resultStations.Stations[31].Should().BeEquivalentTo(new { Id = Guid.Parse("11d96cde-06ba-4160-8200-cef94ccdd6d8"), Name = "Upton Park" });
        resultStations.Stations[32].Should().BeEquivalentTo(new { Id = Guid.Parse("66d453d7-94b8-4969-abaf-ba04036f850e"), Name = "East Ham" });
        resultStations.Stations[33].Should().BeEquivalentTo(new { Id = Guid.Parse("1c5faedb-30a6-4957-a8f7-6cdc702f4f68"), Name = "Barking" });
        resultStations.Stations[34].Should().BeEquivalentTo(new { Id = Guid.Parse("77f3aa5d-6a53-451a-9c65-3531ae5a7fc8"), Name = "Upney" });
        resultStations.Stations[35].Should().BeEquivalentTo(new { Id = Guid.Parse("f2ae9bb5-ccc5-4a4f-ab6b-94cff25f9e6c"), Name = "Becontree" });
        resultStations.Stations[36].Should().BeEquivalentTo(new { Id = Guid.Parse("d36e54a7-6651-49e8-ae5a-79d0700880f1"), Name = "Dagenham Heathway" });
        resultStations.Stations[37].Should().BeEquivalentTo(new { Id = Guid.Parse("50a61507-b3d8-4c0d-ac2d-916197e31392"), Name = "Dagenham East" });
        resultStations.Stations[38].Should().BeEquivalentTo(new { Id = Guid.Parse("e4b6eac7-93a1-4d02-a2bc-defa7802facf"), Name = "Elm Park" });
        resultStations.Stations[39].Should().BeEquivalentTo(new { Id = Guid.Parse("7dadbd81-a249-4ec5-b681-b99218f139fd"), Name = "Hornchurch" });
        resultStations.Stations[40].Should().BeEquivalentTo(new { Id = Guid.Parse("acf5008c-13f0-4439-91df-a45ab9bfbbd0"), Name = "Upminster Bridge" });
        resultStations.Stations[41].Should().BeEquivalentTo(new { Id = Guid.Parse("2a423032-a8ab-40dc-8b3b-44085632d1a9"), Name = "Upminster" });

        resultStations = result.ValidRoutes[4];
        resultStations.Stations.Count.Should().Be(15);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("10c300fa-acc5-4701-8a3d-ba27a3327696"), Name = "Richmond" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("afab4380-82ea-49ce-bf83-15bb3258110b"), Name = "Edgware Road" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("10c300fa-acc5-4701-8a3d-ba27a3327696"), Name = "Richmond" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("92a0ee87-b829-4669-9482-fb8620455b4b"), Name = "Kew Gardens" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("4a1b2adb-a70e-42d1-b7e7-a2845ca98e7c"), Name = "Gunnersbury" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("612f4d89-3086-4e1b-9771-d0e748718eb8"), Name = "Turnham Green" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("07fbc482-414a-4f9c-a30f-c4cf9f752e38"), Name = "Stamford Brook" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("20f6b18f-59d0-4139-93df-2dcc022eec77"), Name = "Ravenscourt Park" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("bdf7bc22-9e0b-4bfc-8abe-8130f5c462c8"), Name = "Hammersmith" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("6ea41ee3-5e14-4830-af0a-02d2bd9bcf98"), Name = "Barons Court" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("407f81fa-3a8f-4af8-a707-6042ae296236"), Name = "West Kensington" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("03c1cead-6d76-40f7-b67d-0eecef00220b"), Name = "Earl's Court" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("d60ce047-7589-4458-96b7-300ae3737941"), Name = "High Street Kensington" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("cd981628-5257-4fd1-a657-7168613eb50d"), Name = "Notting Hill Gate" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("cb25b18f-4fee-4b93-a63e-79921a6cfc62"), Name = "Bayswater" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("6a52e0a7-0801-4f1a-85bd-a34f70955052"), Name = "Paddington" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("afab4380-82ea-49ce-bf83-15bb3258110b"), Name = "Edgware Road" });

        resultStations = result.ValidRoutes[5];
        resultStations.Stations.Count.Should().Be(41);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("73a238b3-22e1-44f1-aa28-d0a7b41cea60"), Name = "Wimbledon" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("2a423032-a8ab-40dc-8b3b-44085632d1a9"), Name = "Upminster" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("73a238b3-22e1-44f1-aa28-d0a7b41cea60"), Name = "Wimbledon" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("48c38323-7062-4ba8-839b-02b0935032ea"), Name = "Wimbledon Park" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("17962928-f06a-4f71-8d8b-f83fdf6ea544"), Name = "Southfields" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("960db567-1340-4c5d-8c90-4b355ce61c17"), Name = "East Putney" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("b5085a0b-1acf-43e9-a430-814f2b2e7ed4"), Name = "Putney Bridge" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("e8ed490f-cda8-48eb-944b-853adf60f940"), Name = "Parsons Green" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("1062ff4a-e84d-4f71-9c54-ed2936bbc889"), Name = "Fulham Broadway" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("6a65894f-41da-47e2-bfcc-68d877d2a9b2"), Name = "West Brompton" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("03c1cead-6d76-40f7-b67d-0eecef00220b"), Name = "Earl's Court" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("94b2e2cc-4150-4ab0-b387-21e2a13960e1"), Name = "Gloucester Road" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("4afcf7aa-f5ee-499f-9484-38e0b0c0af0b"), Name = "South Kensington" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("ab3c57e2-61cb-470d-8644-7926e2896c50"), Name = "Sloane Square" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("9d0f8c47-3708-489a-99b3-1a6d960341e6"), Name = "Victoria" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("3fdb5fa9-f04e-4348-97e9-a4e9d9948ca4"), Name = "St.James's Park" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("d58a55c8-bd8d-45f7-a7a2-c0bd0096afca"), Name = "Westminster" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("ae58d763-b367-4b09-9f1d-3be50467f47f"), Name = "Embankment" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("a277af62-5304-4154-86cb-344933d8c7f3"), Name = "Temple" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("dba0d82b-4618-4fb4-9b79-cf2a22c9d9c1"), Name = "Blackfriars" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("4e490fff-0a4c-459b-9a52-c63d4bb3ceba"), Name = "Mansion House" });
        resultStations.Stations[19].Should().BeEquivalentTo(new { Id = Guid.Parse("65d97039-79a9-41e2-9351-21ca5d866b14"), Name = "Cannon Street" });
        resultStations.Stations[20].Should().BeEquivalentTo(new { Id = Guid.Parse("5514e4ab-7cd9-4e07-8fe6-57ecf30b69ea"), Name = "Monument" });
        resultStations.Stations[21].Should().BeEquivalentTo(new { Id = Guid.Parse("e82d6990-68ae-43e2-a353-71135b2075e3"), Name = "Tower Hill" });
        resultStations.Stations[22].Should().BeEquivalentTo(new { Id = Guid.Parse("e82e025a-1362-4f11-9da9-9625cd8ac71d"), Name = "Aldgate East" });
        resultStations.Stations[23].Should().BeEquivalentTo(new { Id = Guid.Parse("9787df93-f917-4890-9e0b-8b36e795bf9b"), Name = "Whitechapel" });
        resultStations.Stations[24].Should().BeEquivalentTo(new { Id = Guid.Parse("4bb7447a-4182-4ba7-996d-07a2bcc119d1"), Name = "Stepney Green" });
        resultStations.Stations[25].Should().BeEquivalentTo(new { Id = Guid.Parse("73bce1de-143f-4903-928a-c34ceb3db42e"), Name = "Mile End" });
        resultStations.Stations[26].Should().BeEquivalentTo(new { Id = Guid.Parse("3db408d6-248a-4ef7-8486-203e87cc408a"), Name = "Bow Road" });
        resultStations.Stations[27].Should().BeEquivalentTo(new { Id = Guid.Parse("a391396c-6921-4202-ace2-2d5033bfac1f"), Name = "Bromley By Bow" });
        resultStations.Stations[28].Should().BeEquivalentTo(new { Id = Guid.Parse("968bc258-138c-45cf-83c0-599705285d25"), Name = "West Ham" });
        resultStations.Stations[29].Should().BeEquivalentTo(new { Id = Guid.Parse("b9b356ad-31ac-41da-998d-72dcca4c621f"), Name = "Plaistow" });
        resultStations.Stations[30].Should().BeEquivalentTo(new { Id = Guid.Parse("11d96cde-06ba-4160-8200-cef94ccdd6d8"), Name = "Upton Park" });
        resultStations.Stations[31].Should().BeEquivalentTo(new { Id = Guid.Parse("66d453d7-94b8-4969-abaf-ba04036f850e"), Name = "East Ham" });
        resultStations.Stations[32].Should().BeEquivalentTo(new { Id = Guid.Parse("1c5faedb-30a6-4957-a8f7-6cdc702f4f68"), Name = "Barking" });
        resultStations.Stations[33].Should().BeEquivalentTo(new { Id = Guid.Parse("77f3aa5d-6a53-451a-9c65-3531ae5a7fc8"), Name = "Upney" });
        resultStations.Stations[34].Should().BeEquivalentTo(new { Id = Guid.Parse("f2ae9bb5-ccc5-4a4f-ab6b-94cff25f9e6c"), Name = "Becontree" });
        resultStations.Stations[35].Should().BeEquivalentTo(new { Id = Guid.Parse("d36e54a7-6651-49e8-ae5a-79d0700880f1"), Name = "Dagenham Heathway" });
        resultStations.Stations[36].Should().BeEquivalentTo(new { Id = Guid.Parse("50a61507-b3d8-4c0d-ac2d-916197e31392"), Name = "Dagenham East" });
        resultStations.Stations[37].Should().BeEquivalentTo(new { Id = Guid.Parse("e4b6eac7-93a1-4d02-a2bc-defa7802facf"), Name = "Elm Park" });
        resultStations.Stations[38].Should().BeEquivalentTo(new { Id = Guid.Parse("7dadbd81-a249-4ec5-b681-b99218f139fd"), Name = "Hornchurch" });
        resultStations.Stations[39].Should().BeEquivalentTo(new { Id = Guid.Parse("acf5008c-13f0-4439-91df-a45ab9bfbbd0"), Name = "Upminster Bridge" });
        resultStations.Stations[40].Should().BeEquivalentTo(new { Id = Guid.Parse("2a423032-a8ab-40dc-8b3b-44085632d1a9"), Name = "Upminster" });

        resultStations = result.ValidRoutes[6];
        resultStations.Stations.Count.Should().Be(14);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("73a238b3-22e1-44f1-aa28-d0a7b41cea60"), Name = "Wimbledon" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("afab4380-82ea-49ce-bf83-15bb3258110b"), Name = "Edgware Road" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("73a238b3-22e1-44f1-aa28-d0a7b41cea60"), Name = "Wimbledon" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("48c38323-7062-4ba8-839b-02b0935032ea"), Name = "Wimbledon Park" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("17962928-f06a-4f71-8d8b-f83fdf6ea544"), Name = "Southfields" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("960db567-1340-4c5d-8c90-4b355ce61c17"), Name = "East Putney" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("b5085a0b-1acf-43e9-a430-814f2b2e7ed4"), Name = "Putney Bridge" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("e8ed490f-cda8-48eb-944b-853adf60f940"), Name = "Parsons Green" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("1062ff4a-e84d-4f71-9c54-ed2936bbc889"), Name = "Fulham Broadway" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("6a65894f-41da-47e2-bfcc-68d877d2a9b2"), Name = "West Brompton" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("03c1cead-6d76-40f7-b67d-0eecef00220b"), Name = "Earl's Court" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("d60ce047-7589-4458-96b7-300ae3737941"), Name = "High Street Kensington" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("cd981628-5257-4fd1-a657-7168613eb50d"), Name = "Notting Hill Gate" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("cb25b18f-4fee-4b93-a63e-79921a6cfc62"), Name = "Bayswater" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("6a52e0a7-0801-4f1a-85bd-a34f70955052"), Name = "Paddington" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("afab4380-82ea-49ce-bf83-15bb3258110b"), Name = "Edgware Road" });
    }

    [Fact]
    public void RouteRepository_DLR_Routes_Correct()
    {
        var result = _repository.Lines[Guid.Parse("85b9e52c-697b-4db8-876f-600423ffe176")];

        result.Name.Should().Be("DLR");
        result.ValidRoutes.Count.Should().Be(3);

        var resultStations = result.ValidRoutes[0];
        resultStations.Stations.Count.Should().Be(15);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("aaedc653-e766-4d6b-87e2-4c87322971ef"), Name = "Bank" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("ae8da6d3-57bc-4291-a3f2-79dd96eac775"), Name = "Lewisham" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("aaedc653-e766-4d6b-87e2-4c87322971ef"), Name = "Bank" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("83c12f73-4e47-4aab-a10c-ff445a458a33"), Name = "Shadwell" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("72555c4d-4c54-4ddf-9094-aed433b32224"), Name = "Limehouse" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("25c71d0e-7103-4f7e-b40c-19447915ffe8"), Name = "Westferry" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("5c15a8f5-a21d-4567-97a4-3cbc095d2298"), Name = "Canary Wharf" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("ccf4fc3b-3f2d-4da7-b756-80307b278b65"), Name = "Heron Quays" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("43cfa7d4-cd92-45f2-aea9-24cbae727db8"), Name = "South Quay" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("739909b9-9cb9-4806-9d50-f1136edf69e7"), Name = "Crossharbour" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("c2178257-3ee0-46a8-a029-afeffda629a0"), Name = "Mudchute" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("7b99c143-e091-4fcc-94e2-3b161fb1f6cd"), Name = "Island Gardens" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("d50173b7-e272-44e6-9b0c-0b0f0bcc0c09"), Name = "Cutty Sark" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("ff2c1d03-e13a-4d1d-b0d1-9f421e5b3ff3"), Name = "Greenwich" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("f4718576-9e9b-4b6a-b0ca-77bd690d27de"), Name = "Deptford Bridge" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("7fa419bf-33d1-4944-92ce-020cb43c9d64"), Name = "Elverson Road" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("ae8da6d3-57bc-4291-a3f2-79dd96eac775"), Name = "Lewisham" });

        resultStations = result.ValidRoutes[1];
        resultStations.Stations.Count.Should().Be(13);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("aaedc653-e766-4d6b-87e2-4c87322971ef"), Name = "Bank" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("b68d33dc-e594-439f-aaed-8446f96f8f13"), Name = "Woolwich Arsenal" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("aaedc653-e766-4d6b-87e2-4c87322971ef"), Name = "Bank" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("83c12f73-4e47-4aab-a10c-ff445a458a33"), Name = "Shadwell" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("72555c4d-4c54-4ddf-9094-aed433b32224"), Name = "Limehouse" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("25c71d0e-7103-4f7e-b40c-19447915ffe8"), Name = "Westferry" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("f90a3bb8-4060-4dc4-acb0-973561e6e0a5"), Name = "Poplar" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("ac13f225-4486-4a41-ba5f-0b1de1b7fa04"), Name = "Blackwall" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("ae0de89e-691e-4ad8-b3d4-b5278cfd923a"), Name = "East India" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("752cd9c1-bead-404f-b12a-aa93c212f2c2"), Name = "Canning Town" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("5008326b-8bd5-4cf1-935f-ab370c653354"), Name = "West Silvertown" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("c130659d-1ebb-4b08-bf44-a9ad1f33936c"), Name = "Pontoon Dock" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("d9a70c10-0553-4830-bb13-0292c4c104bc"), Name = "London City Airport" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("58b068c2-013d-439a-977c-c072db3c7754"), Name = "King George V" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("b68d33dc-e594-439f-aaed-8446f96f8f13"), Name = "Woolwich Arsenal" });

        resultStations = result.ValidRoutes[2];
        resultStations.Stations.Count.Should().Be(9);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("b02ebcb8-83a4-48e1-85d8-6e3fb21fa058"), Name = "Stratford" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("5c15a8f5-a21d-4567-97a4-3cbc095d2298"), Name = "Canary Wharf" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("b02ebcb8-83a4-48e1-85d8-6e3fb21fa058"), Name = "Stratford" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("75e66a83-fc1e-4d10-b48c-8bfc60cb3648"), Name = "Pudding Mill Lane" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("d82d33af-3f55-46b0-b767-ff0f81128b48"), Name = "Bow Church" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("0907197c-dbee-4456-a535-04dd7ac31686"), Name = "Devons Road" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("18e58b30-0ed1-4c08-82a3-ee9cf97530a5"), Name = "Langdon Park" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("0568a75b-348a-4def-848f-ba4ec7cd90db"), Name = "All Saints" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("f90a3bb8-4060-4dc4-acb0-973561e6e0a5"), Name = "Poplar" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("dee286e7-8a98-4976-82cf-9fc01651c889"), Name = "West India Quay" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("5c15a8f5-a21d-4567-97a4-3cbc095d2298"), Name = "Canary Wharf" });
    }

    [Fact]
    public void RouteRepository_Elizabeth_Routes_Correct()
    {
        var result = _repository.Lines[Guid.Parse("d232ac77-6032-4658-aed8-e47f89b79353")];

        result.Name.Should().Be("Elizabeth");
        result.ValidRoutes.Count.Should().Be(9);

        var resultStations = result.ValidRoutes[0];
        resultStations.Stations.Count.Should().Be(18);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("732544f8-e21a-4a19-a118-01e2d25ed159"), Name = "Abbey Wood" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("a7ce86a8-d1a2-4c40-a8f0-0ead55233d5d"), Name = "Heathrow Terminal 4" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("732544f8-e21a-4a19-a118-01e2d25ed159"), Name = "Abbey Wood" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("6b349bb9-baa3-45d7-839e-38bd6024d466"), Name = "Woolwich" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("de485292-f6bd-4ccf-9a93-1afd852017e2"), Name = "Custom House" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("5c15a8f5-a21d-4567-97a4-3cbc095d2298"), Name = "Canary Wharf" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("9787df93-f917-4890-9e0b-8b36e795bf9b"), Name = "Whitechapel" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("db101bcd-350e-4485-b875-7ac2c8c1b6cc"), Name = "Liverpool Street" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("bf6a3d1b-2af9-4b1c-b696-fc6f8c8cecde"), Name = "Farringdon" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("70f156b3-520a-46e7-a35d-3f603c6ab5b7"), Name = "Tottenham Court Road" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("d2621069-fea8-4b31-8b56-16048f6b949d"), Name = "Bond Street" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("6a52e0a7-0801-4f1a-85bd-a34f70955052"), Name = "Paddington" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("6ec53ddb-28d4-45f7-ae4b-948789c5aa8e"), Name = "Acton Main Line" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("2e67db42-0ae1-4406-9df5-666e8b64df28"), Name = "Ealing Broadway" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("2b96e9be-3a2b-4ef5-8cb1-5a2706bc8f50"), Name = "West Ealing" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("10422f80-4cf5-4be1-ace8-a7ee5d272f01"), Name = "Hanwell" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("0e5e8de3-1549-4b30-8c09-abe2a242df4c"), Name = "Southall" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("ed82a619-cabf-4aed-a00d-664e179f0828"), Name = "Hayes & Harlington" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("d4ecc02a-082d-4408-9f4f-2e4be6655681"), Name = "Heathrow Terminals 2 & 3" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("a7ce86a8-d1a2-4c40-a8f0-0ead55233d5d"), Name = "Heathrow Terminal 4" });

        resultStations = result.ValidRoutes[1];
        resultStations.Stations.Count.Should().Be(18);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("732544f8-e21a-4a19-a118-01e2d25ed159"), Name = "Abbey Wood" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("7c583322-8051-4e43-a533-e372bdb3f875"), Name = "Heathrow Terminal 5" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("732544f8-e21a-4a19-a118-01e2d25ed159"), Name = "Abbey Wood" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("6b349bb9-baa3-45d7-839e-38bd6024d466"), Name = "Woolwich" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("de485292-f6bd-4ccf-9a93-1afd852017e2"), Name = "Custom House" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("5c15a8f5-a21d-4567-97a4-3cbc095d2298"), Name = "Canary Wharf" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("9787df93-f917-4890-9e0b-8b36e795bf9b"), Name = "Whitechapel" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("db101bcd-350e-4485-b875-7ac2c8c1b6cc"), Name = "Liverpool Street" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("bf6a3d1b-2af9-4b1c-b696-fc6f8c8cecde"), Name = "Farringdon" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("70f156b3-520a-46e7-a35d-3f603c6ab5b7"), Name = "Tottenham Court Road" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("d2621069-fea8-4b31-8b56-16048f6b949d"), Name = "Bond Street" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("6a52e0a7-0801-4f1a-85bd-a34f70955052"), Name = "Paddington" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("6ec53ddb-28d4-45f7-ae4b-948789c5aa8e"), Name = "Acton Main Line" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("2e67db42-0ae1-4406-9df5-666e8b64df28"), Name = "Ealing Broadway" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("2b96e9be-3a2b-4ef5-8cb1-5a2706bc8f50"), Name = "West Ealing" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("10422f80-4cf5-4be1-ace8-a7ee5d272f01"), Name = "Hanwell" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("0e5e8de3-1549-4b30-8c09-abe2a242df4c"), Name = "Southall" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("ed82a619-cabf-4aed-a00d-664e179f0828"), Name = "Hayes & Harlington" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("d4ecc02a-082d-4408-9f4f-2e4be6655681"), Name = "Heathrow Terminals 2 & 3" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("7c583322-8051-4e43-a533-e372bdb3f875"), Name = "Heathrow Terminal 5" });

        resultStations = result.ValidRoutes[2];
        resultStations.Stations.Count.Should().Be(25);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("732544f8-e21a-4a19-a118-01e2d25ed159"), Name = "Abbey Wood" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("a65fd8d9-5aee-493c-84c7-5492c092b41a"), Name = "Reading" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("732544f8-e21a-4a19-a118-01e2d25ed159"), Name = "Abbey Wood" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("6b349bb9-baa3-45d7-839e-38bd6024d466"), Name = "Woolwich" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("de485292-f6bd-4ccf-9a93-1afd852017e2"), Name = "Custom House" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("5c15a8f5-a21d-4567-97a4-3cbc095d2298"), Name = "Canary Wharf" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("9787df93-f917-4890-9e0b-8b36e795bf9b"), Name = "Whitechapel" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("db101bcd-350e-4485-b875-7ac2c8c1b6cc"), Name = "Liverpool Street" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("bf6a3d1b-2af9-4b1c-b696-fc6f8c8cecde"), Name = "Farringdon" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("70f156b3-520a-46e7-a35d-3f603c6ab5b7"), Name = "Tottenham Court Road" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("d2621069-fea8-4b31-8b56-16048f6b949d"), Name = "Bond Street" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("6a52e0a7-0801-4f1a-85bd-a34f70955052"), Name = "Paddington" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("6ec53ddb-28d4-45f7-ae4b-948789c5aa8e"), Name = "Acton Main Line" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("2e67db42-0ae1-4406-9df5-666e8b64df28"), Name = "Ealing Broadway" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("2b96e9be-3a2b-4ef5-8cb1-5a2706bc8f50"), Name = "West Ealing" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("10422f80-4cf5-4be1-ace8-a7ee5d272f01"), Name = "Hanwell" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("0e5e8de3-1549-4b30-8c09-abe2a242df4c"), Name = "Southall" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("ed82a619-cabf-4aed-a00d-664e179f0828"), Name = "Hayes & Harlington" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("e8d66c5c-fb13-4374-b2e1-3ef2c11f7424"), Name = "West Drayton" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("32b6b31c-40a2-4cb9-9855-a873b25b5cd6"), Name = "Iver" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("0357b810-e7bb-491f-961c-6f66d0aae40c"), Name = "Langley" });
        resultStations.Stations[19].Should().BeEquivalentTo(new { Id = Guid.Parse("cf320e21-7418-466d-a841-1d333fa45a00"), Name = "Slough" });
        resultStations.Stations[20].Should().BeEquivalentTo(new { Id = Guid.Parse("6695ce27-86a1-4489-82e7-de7992180505"), Name = "Burnham" });
        resultStations.Stations[21].Should().BeEquivalentTo(new { Id = Guid.Parse("e89b77ea-ebcc-48ec-9e14-e2ed15857d79"), Name = "Taplow" });
        resultStations.Stations[22].Should().BeEquivalentTo(new { Id = Guid.Parse("cbc3317b-7415-4ca8-9787-829dda92302f"), Name = "Maidenhead" });
        resultStations.Stations[23].Should().BeEquivalentTo(new { Id = Guid.Parse("53fd736d-6432-44b1-8957-8e3194674644"), Name = "Twyford" });
        resultStations.Stations[24].Should().BeEquivalentTo(new { Id = Guid.Parse("a65fd8d9-5aee-493c-84c7-5492c092b41a"), Name = "Reading" });

        resultStations = result.ValidRoutes[3];
        resultStations.Stations.Count.Should().Be(9);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("6a52e0a7-0801-4f1a-85bd-a34f70955052"), Name = "Paddington" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("a7ce86a8-d1a2-4c40-a8f0-0ead55233d5d"), Name = "Heathrow Terminal 4" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("6a52e0a7-0801-4f1a-85bd-a34f70955052"), Name = "Paddington" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("6ec53ddb-28d4-45f7-ae4b-948789c5aa8e"), Name = "Acton Main Line" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("2e67db42-0ae1-4406-9df5-666e8b64df28"), Name = "Ealing Broadway" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("2b96e9be-3a2b-4ef5-8cb1-5a2706bc8f50"), Name = "West Ealing" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("10422f80-4cf5-4be1-ace8-a7ee5d272f01"), Name = "Hanwell" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("0e5e8de3-1549-4b30-8c09-abe2a242df4c"), Name = "Southall" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("ed82a619-cabf-4aed-a00d-664e179f0828"), Name = "Hayes & Harlington" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("d4ecc02a-082d-4408-9f4f-2e4be6655681"), Name = "Heathrow Terminals 2 & 3" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("a7ce86a8-d1a2-4c40-a8f0-0ead55233d5d"), Name = "Heathrow Terminal 4" });

        resultStations = result.ValidRoutes[4];
        resultStations.Stations.Count.Should().Be(9);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("6a52e0a7-0801-4f1a-85bd-a34f70955052"), Name = "Paddington" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("7c583322-8051-4e43-a533-e372bdb3f875"), Name = "Heathrow Terminal 5" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("6a52e0a7-0801-4f1a-85bd-a34f70955052"), Name = "Paddington" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("6ec53ddb-28d4-45f7-ae4b-948789c5aa8e"), Name = "Acton Main Line" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("2e67db42-0ae1-4406-9df5-666e8b64df28"), Name = "Ealing Broadway" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("2b96e9be-3a2b-4ef5-8cb1-5a2706bc8f50"), Name = "West Ealing" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("10422f80-4cf5-4be1-ace8-a7ee5d272f01"), Name = "Hanwell" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("0e5e8de3-1549-4b30-8c09-abe2a242df4c"), Name = "Southall" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("ed82a619-cabf-4aed-a00d-664e179f0828"), Name = "Hayes & Harlington" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("d4ecc02a-082d-4408-9f4f-2e4be6655681"), Name = "Heathrow Terminals 2 & 3" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("7c583322-8051-4e43-a533-e372bdb3f875"), Name = "Heathrow Terminal 5" });

        resultStations = result.ValidRoutes[5];
        resultStations.Stations.Count.Should().Be(16);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("6a52e0a7-0801-4f1a-85bd-a34f70955052"), Name = "Paddington" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("a65fd8d9-5aee-493c-84c7-5492c092b41a"), Name = "Reading" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("6a52e0a7-0801-4f1a-85bd-a34f70955052"), Name = "Paddington" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("6ec53ddb-28d4-45f7-ae4b-948789c5aa8e"), Name = "Acton Main Line" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("2e67db42-0ae1-4406-9df5-666e8b64df28"), Name = "Ealing Broadway" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("2b96e9be-3a2b-4ef5-8cb1-5a2706bc8f50"), Name = "West Ealing" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("10422f80-4cf5-4be1-ace8-a7ee5d272f01"), Name = "Hanwell" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("0e5e8de3-1549-4b30-8c09-abe2a242df4c"), Name = "Southall" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("ed82a619-cabf-4aed-a00d-664e179f0828"), Name = "Hayes & Harlington" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("e8d66c5c-fb13-4374-b2e1-3ef2c11f7424"), Name = "West Drayton" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("32b6b31c-40a2-4cb9-9855-a873b25b5cd6"), Name = "Iver" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("0357b810-e7bb-491f-961c-6f66d0aae40c"), Name = "Langley" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("cf320e21-7418-466d-a841-1d333fa45a00"), Name = "Slough" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("6695ce27-86a1-4489-82e7-de7992180505"), Name = "Burnham" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("e89b77ea-ebcc-48ec-9e14-e2ed15857d79"), Name = "Taplow" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("cbc3317b-7415-4ca8-9787-829dda92302f"), Name = "Maidenhead" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("53fd736d-6432-44b1-8957-8e3194674644"), Name = "Twyford" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("a65fd8d9-5aee-493c-84c7-5492c092b41a"), Name = "Reading" });

        resultStations = result.ValidRoutes[6];
        resultStations.Stations.Count.Should().Be(14);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("ae9ca6c9-96af-4333-aaeb-b54bfc4cf422"), Name = "Shenfield" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("db101bcd-350e-4485-b875-7ac2c8c1b6cc"), Name = "Liverpool Street" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("ae9ca6c9-96af-4333-aaeb-b54bfc4cf422"), Name = "Shenfield" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("0c1cc3c9-d62e-4d27-9925-9450988310dd"), Name = "Brentwood" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("1c1d5cb8-ed89-4eb2-b866-7d0b9968ac5f"), Name = "Harold Wood" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("ca60d6b4-8aaa-4273-9cf8-ce4c634e75f3"), Name = "Gidea Park" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("9df16581-f3e7-4ab6-a615-5460e2837c1d"), Name = "Romford" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("cc9a25d6-ecab-47c4-94fb-0c9fe406b3e1"), Name = "Chadwell Heath" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("482bf23c-1c1e-4bde-be59-7a7768d3edc4"), Name = "Goodmayes" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("b58d9945-ed14-4183-a465-2fe7b8ce4ab5"), Name = "Seven Kings" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("a0d08449-9cd2-4316-9a52-497482b07150"), Name = "Ilford" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("db9fad9f-799e-46aa-a0ac-cc099a9260d5"), Name = "Manor Park" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("21534500-bdeb-4202-8e71-f2f741a42afe"), Name = "Forest Gate" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("9861edc5-b909-4a9c-b03b-50bf7627197a"), Name = "Maryland" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("b02ebcb8-83a4-48e1-85d8-6e3fb21fa058"), Name = "Stratford" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("db101bcd-350e-4485-b875-7ac2c8c1b6cc"), Name = "Liverpool Street" });

        resultStations = result.ValidRoutes[7];
        resultStations.Stations.Count.Should().Be(27);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("ae9ca6c9-96af-4333-aaeb-b54bfc4cf422"), Name = "Shenfield" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("a7ce86a8-d1a2-4c40-a8f0-0ead55233d5d"), Name = "Heathrow Terminal 4" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("ae9ca6c9-96af-4333-aaeb-b54bfc4cf422"), Name = "Shenfield" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("0c1cc3c9-d62e-4d27-9925-9450988310dd"), Name = "Brentwood" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("1c1d5cb8-ed89-4eb2-b866-7d0b9968ac5f"), Name = "Harold Wood" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("ca60d6b4-8aaa-4273-9cf8-ce4c634e75f3"), Name = "Gidea Park" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("9df16581-f3e7-4ab6-a615-5460e2837c1d"), Name = "Romford" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("cc9a25d6-ecab-47c4-94fb-0c9fe406b3e1"), Name = "Chadwell Heath" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("482bf23c-1c1e-4bde-be59-7a7768d3edc4"), Name = "Goodmayes" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("b58d9945-ed14-4183-a465-2fe7b8ce4ab5"), Name = "Seven Kings" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("a0d08449-9cd2-4316-9a52-497482b07150"), Name = "Ilford" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("db9fad9f-799e-46aa-a0ac-cc099a9260d5"), Name = "Manor Park" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("21534500-bdeb-4202-8e71-f2f741a42afe"), Name = "Forest Gate" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("9861edc5-b909-4a9c-b03b-50bf7627197a"), Name = "Maryland" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("b02ebcb8-83a4-48e1-85d8-6e3fb21fa058"), Name = "Stratford" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("9787df93-f917-4890-9e0b-8b36e795bf9b"), Name = "Whitechapel" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("db101bcd-350e-4485-b875-7ac2c8c1b6cc"), Name = "Liverpool Street" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("bf6a3d1b-2af9-4b1c-b696-fc6f8c8cecde"), Name = "Farringdon" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("70f156b3-520a-46e7-a35d-3f603c6ab5b7"), Name = "Tottenham Court Road" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("d2621069-fea8-4b31-8b56-16048f6b949d"), Name = "Bond Street" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("6a52e0a7-0801-4f1a-85bd-a34f70955052"), Name = "Paddington" });
        resultStations.Stations[19].Should().BeEquivalentTo(new { Id = Guid.Parse("6ec53ddb-28d4-45f7-ae4b-948789c5aa8e"), Name = "Acton Main Line" });
        resultStations.Stations[20].Should().BeEquivalentTo(new { Id = Guid.Parse("2e67db42-0ae1-4406-9df5-666e8b64df28"), Name = "Ealing Broadway" });
        resultStations.Stations[21].Should().BeEquivalentTo(new { Id = Guid.Parse("2b96e9be-3a2b-4ef5-8cb1-5a2706bc8f50"), Name = "West Ealing" });
        resultStations.Stations[22].Should().BeEquivalentTo(new { Id = Guid.Parse("10422f80-4cf5-4be1-ace8-a7ee5d272f01"), Name = "Hanwell" });
        resultStations.Stations[23].Should().BeEquivalentTo(new { Id = Guid.Parse("0e5e8de3-1549-4b30-8c09-abe2a242df4c"), Name = "Southall" });
        resultStations.Stations[24].Should().BeEquivalentTo(new { Id = Guid.Parse("ed82a619-cabf-4aed-a00d-664e179f0828"), Name = "Hayes & Harlington" });
        resultStations.Stations[25].Should().BeEquivalentTo(new { Id = Guid.Parse("d4ecc02a-082d-4408-9f4f-2e4be6655681"), Name = "Heathrow Terminals 2 & 3" });
        resultStations.Stations[26].Should().BeEquivalentTo(new { Id = Guid.Parse("a7ce86a8-d1a2-4c40-a8f0-0ead55233d5d"), Name = "Heathrow Terminal 4" });

        resultStations = result.ValidRoutes[8];
        resultStations.Stations.Count.Should().Be(27);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("ae9ca6c9-96af-4333-aaeb-b54bfc4cf422"), Name = "Shenfield" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("7c583322-8051-4e43-a533-e372bdb3f875"), Name = "Heathrow Terminal 5" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("ae9ca6c9-96af-4333-aaeb-b54bfc4cf422"), Name = "Shenfield" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("0c1cc3c9-d62e-4d27-9925-9450988310dd"), Name = "Brentwood" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("1c1d5cb8-ed89-4eb2-b866-7d0b9968ac5f"), Name = "Harold Wood" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("ca60d6b4-8aaa-4273-9cf8-ce4c634e75f3"), Name = "Gidea Park" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("9df16581-f3e7-4ab6-a615-5460e2837c1d"), Name = "Romford" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("cc9a25d6-ecab-47c4-94fb-0c9fe406b3e1"), Name = "Chadwell Heath" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("482bf23c-1c1e-4bde-be59-7a7768d3edc4"), Name = "Goodmayes" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("b58d9945-ed14-4183-a465-2fe7b8ce4ab5"), Name = "Seven Kings" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("a0d08449-9cd2-4316-9a52-497482b07150"), Name = "Ilford" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("db9fad9f-799e-46aa-a0ac-cc099a9260d5"), Name = "Manor Park" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("21534500-bdeb-4202-8e71-f2f741a42afe"), Name = "Forest Gate" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("9861edc5-b909-4a9c-b03b-50bf7627197a"), Name = "Maryland" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("b02ebcb8-83a4-48e1-85d8-6e3fb21fa058"), Name = "Stratford" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("9787df93-f917-4890-9e0b-8b36e795bf9b"), Name = "Whitechapel" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("db101bcd-350e-4485-b875-7ac2c8c1b6cc"), Name = "Liverpool Street" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("bf6a3d1b-2af9-4b1c-b696-fc6f8c8cecde"), Name = "Farringdon" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("70f156b3-520a-46e7-a35d-3f603c6ab5b7"), Name = "Tottenham Court Road" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("d2621069-fea8-4b31-8b56-16048f6b949d"), Name = "Bond Street" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("6a52e0a7-0801-4f1a-85bd-a34f70955052"), Name = "Paddington" });
        resultStations.Stations[19].Should().BeEquivalentTo(new { Id = Guid.Parse("6ec53ddb-28d4-45f7-ae4b-948789c5aa8e"), Name = "Acton Main Line" });
        resultStations.Stations[20].Should().BeEquivalentTo(new { Id = Guid.Parse("2e67db42-0ae1-4406-9df5-666e8b64df28"), Name = "Ealing Broadway" });
        resultStations.Stations[21].Should().BeEquivalentTo(new { Id = Guid.Parse("2b96e9be-3a2b-4ef5-8cb1-5a2706bc8f50"), Name = "West Ealing" });
        resultStations.Stations[22].Should().BeEquivalentTo(new { Id = Guid.Parse("10422f80-4cf5-4be1-ace8-a7ee5d272f01"), Name = "Hanwell" });
        resultStations.Stations[23].Should().BeEquivalentTo(new { Id = Guid.Parse("0e5e8de3-1549-4b30-8c09-abe2a242df4c"), Name = "Southall" });
        resultStations.Stations[24].Should().BeEquivalentTo(new { Id = Guid.Parse("ed82a619-cabf-4aed-a00d-664e179f0828"), Name = "Hayes & Harlington" });
        resultStations.Stations[25].Should().BeEquivalentTo(new { Id = Guid.Parse("d4ecc02a-082d-4408-9f4f-2e4be6655681"), Name = "Heathrow Terminals 2 & 3" });
        resultStations.Stations[26].Should().BeEquivalentTo(new { Id = Guid.Parse("7c583322-8051-4e43-a533-e372bdb3f875"), Name = "Heathrow Terminal 5" });
    }

    [Fact]
    public void RouteRepository_HammersmithAndCity_Routes_Correct()
    {
        var result = _repository.Lines[Guid.Parse("14a64b9a-9c65-4d49-8c38-4c1782a73c0a")];

        result.Name.Should().Be("Hammersmith & City");
        result.ValidRoutes.Count.Should().Be(1);

        var resultStations = result.ValidRoutes[0];
        resultStations.Stations.Count.Should().Be(29);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("bdf7bc22-9e0b-4bfc-8abe-8130f5c462c8"), Name = "Hammersmith" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("1c5faedb-30a6-4957-a8f7-6cdc702f4f68"), Name = "Barking" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("bdf7bc22-9e0b-4bfc-8abe-8130f5c462c8"), Name = "Hammersmith" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("67848e8e-cbb2-41d9-85f8-a992435638e7"), Name = "Goldhawk Road" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("2b981aba-c5c5-4d34-93a0-e27bc226cb9e"), Name = "Shepherd's Bush Market" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("3a6ffedf-1039-4206-8739-b922ead8fdfc"), Name = "Wood Lane" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("810e52d6-505d-4440-8ce7-6269184998aa"), Name = "Latimer Road" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("0bb0e1a9-8195-4ea1-95d7-98cb89f72418"), Name = "Ladbroke Grove" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("b72c56eb-b3ec-4dba-b489-fe7418a0990c"), Name = "Westbourne Park" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("d02b6015-3e16-4f90-9421-1c5395595ec9"), Name = "Royal Oak" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("6a52e0a7-0801-4f1a-85bd-a34f70955052"), Name = "Paddington" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("afab4380-82ea-49ce-bf83-15bb3258110b"), Name = "Edgware Road" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("7d89b35f-9a87-49df-98ff-fd98f1f67235"), Name = "Baker Street" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("bd2ef776-2cea-49ad-80c4-a7e6587cc67b"), Name = "Great Portland Street" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("0ec876ed-6a2e-48e3-b8b0-6eb2ab13fc12"), Name = "Euston Square" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("5cf18e37-17e0-46c0-9177-6a5951df26b8"), Name = "King's Cross St.Pancras" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("bf6a3d1b-2af9-4b1c-b696-fc6f8c8cecde"), Name = "Farringdon" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("10972919-8db8-4a6c-aadf-ace0e43b5d8c"), Name = "Barbican" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("9343886e-5b86-4bbc-96bf-2fefe6240060"), Name = "Moorgate" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("db101bcd-350e-4485-b875-7ac2c8c1b6cc"), Name = "Liverpool Street" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("e82e025a-1362-4f11-9da9-9625cd8ac71d"), Name = "Aldgate East" });
        resultStations.Stations[19].Should().BeEquivalentTo(new { Id = Guid.Parse("9787df93-f917-4890-9e0b-8b36e795bf9b"), Name = "Whitechapel" });
        resultStations.Stations[20].Should().BeEquivalentTo(new { Id = Guid.Parse("4bb7447a-4182-4ba7-996d-07a2bcc119d1"), Name = "Stepney Green" });
        resultStations.Stations[21].Should().BeEquivalentTo(new { Id = Guid.Parse("73bce1de-143f-4903-928a-c34ceb3db42e"), Name = "Mile End" });
        resultStations.Stations[22].Should().BeEquivalentTo(new { Id = Guid.Parse("3db408d6-248a-4ef7-8486-203e87cc408a"), Name = "Bow Road" });
        resultStations.Stations[23].Should().BeEquivalentTo(new { Id = Guid.Parse("a391396c-6921-4202-ace2-2d5033bfac1f"), Name = "Bromley By Bow" });
        resultStations.Stations[24].Should().BeEquivalentTo(new { Id = Guid.Parse("968bc258-138c-45cf-83c0-599705285d25"), Name = "West Ham" });
        resultStations.Stations[25].Should().BeEquivalentTo(new { Id = Guid.Parse("b9b356ad-31ac-41da-998d-72dcca4c621f"), Name = "Plaistow" });
        resultStations.Stations[26].Should().BeEquivalentTo(new { Id = Guid.Parse("11d96cde-06ba-4160-8200-cef94ccdd6d8"), Name = "Upton Park" });
        resultStations.Stations[27].Should().BeEquivalentTo(new { Id = Guid.Parse("66d453d7-94b8-4969-abaf-ba04036f850e"), Name = "East Ham" });
        resultStations.Stations[28].Should().BeEquivalentTo(new { Id = Guid.Parse("1c5faedb-30a6-4957-a8f7-6cdc702f4f68"), Name = "Barking" });
    }

    [Fact]
    public void RouteRepository_Jubilee_Routes_Correct()
    {
        var result = _repository.Lines[Guid.Parse("2f0c75a5-8149-49b7-9cc6-32e4a5246d7f")];

        result.Name.Should().Be("Jubilee");
        result.ValidRoutes.Count.Should().Be(1);

        var resultStations = result.ValidRoutes[0];
        resultStations.Stations.Count.Should().Be(27);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("0fe3be5f-dabb-4c45-b6a3-07f5d432c183"), Name = "Stanmore" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("b02ebcb8-83a4-48e1-85d8-6e3fb21fa058"), Name = "Stratford" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("0fe3be5f-dabb-4c45-b6a3-07f5d432c183"), Name = "Stanmore" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("c02f0feb-d9fc-4696-89d7-ebc52c96f0e8"), Name = "Canons Park" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("5059c39d-2492-49a0-9eaa-0a2c6cdfa605"), Name = "Queensbury" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("58ce9379-1d1d-44f5-9142-04943824e132"), Name = "Kingsbury" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("5d1c8be3-f186-401e-a12c-b0d7ef0a6e3f"), Name = "Wembley Park" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("8e72b375-ed8b-4cdc-b448-73a783eeb355"), Name = "Neasden" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("b797191b-9efc-49a7-b048-fe8b23932717"), Name = "Dollis Hill" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("db9da180-7ba2-49d6-90c0-27bace8d6047"), Name = "Willesden Green" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("3686c2bf-12bd-43cf-8975-6891896189ba"), Name = "Kilburn" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("6b2099cd-f9f5-4d37-803c-82571d4fad6b"), Name = "West Hampstead" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("3a3c9204-f090-45fb-b3f5-774a8948248e"), Name = "Finchley Road" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("895aac85-fad1-4e24-8fb7-a5988868b4b9"), Name = "Swiss Cottage" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("02da1648-25ed-41cf-b99b-a2eb9d448380"), Name = "St.John's Wood" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("7d89b35f-9a87-49df-98ff-fd98f1f67235"), Name = "Baker Street" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("d2621069-fea8-4b31-8b56-16048f6b949d"), Name = "Bond Street" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("a88f9ee1-e742-44ea-96b1-467df4a561a2"), Name = "Green Park" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("d58a55c8-bd8d-45f7-a7a2-c0bd0096afca"), Name = "Westminster" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("8cebdb43-8d17-49a2-a06b-1f3513091845"), Name = "Waterloo" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("5e4ec373-90db-4e4c-a10a-e758c7baf433"), Name = "Southwark" });
        resultStations.Stations[19].Should().BeEquivalentTo(new { Id = Guid.Parse("9c8c4a97-c895-4c03-bba7-0f54a3b11bb3"), Name = "London Bridge" });
        resultStations.Stations[20].Should().BeEquivalentTo(new { Id = Guid.Parse("6842d9a0-acc8-4843-a3d3-2e06d03fdcd1"), Name = "Bermondsey" });
        resultStations.Stations[21].Should().BeEquivalentTo(new { Id = Guid.Parse("28cee11a-267d-4170-9cdc-2e7ef7b6ca40"), Name = "Canada Water" });
        resultStations.Stations[22].Should().BeEquivalentTo(new { Id = Guid.Parse("5c15a8f5-a21d-4567-97a4-3cbc095d2298"), Name = "Canary Wharf" });
        resultStations.Stations[23].Should().BeEquivalentTo(new { Id = Guid.Parse("6252902f-7fd2-45a8-a6d5-1f377e88b9be"), Name = "North Greenwich" });
        resultStations.Stations[24].Should().BeEquivalentTo(new { Id = Guid.Parse("752cd9c1-bead-404f-b12a-aa93c212f2c2"), Name = "Canning Town" });
        resultStations.Stations[25].Should().BeEquivalentTo(new { Id = Guid.Parse("968bc258-138c-45cf-83c0-599705285d25"), Name = "West Ham" });
        resultStations.Stations[26].Should().BeEquivalentTo(new { Id = Guid.Parse("b02ebcb8-83a4-48e1-85d8-6e3fb21fa058"), Name = "Stratford" });

    }

    [Fact]
    public void RouteRepository_Liberty_Routes_Correct()
    {
        var result = _repository.Lines[Guid.Parse("1ef96e79-2dab-43b3-b931-6bf9a0495b22")];

        result.Name.Should().Be("Liberty");
        result.ValidRoutes.Count.Should().Be(1);

        var resultStations = result.ValidRoutes[0];
        resultStations.Stations.Count.Should().Be(3);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("9df16581-f3e7-4ab6-a615-5460e2837c1d"), Name = "Romford" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("2a423032-a8ab-40dc-8b3b-44085632d1a9"), Name = "Upminster" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("9df16581-f3e7-4ab6-a615-5460e2837c1d"), Name = "Romford" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("9698c675-16a9-4c68-b027-ee4cd7f239f3"), Name = "Emerson Park" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("2a423032-a8ab-40dc-8b3b-44085632d1a9"), Name = "Upminster" });
    }

    [Fact]
    public void RouteRepository_Lioness_Routes_Correct()
    {
        var result = _repository.Lines[Guid.Parse("1bc14562-671e-4cb8-99c0-f77f3458a03d")];

        result.Name.Should().Be("Lioness");
        result.ValidRoutes.Count.Should().Be(1);

        var resultStations = result.ValidRoutes[0];
        resultStations.Stations.Count.Should().Be(19);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("cacbfade-1233-4922-9caa-aa8b0db9b3da"), Name = "Euston" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("455d4830-bd0d-4962-8869-6443c4c8c452"), Name = "Watford Junction" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("cacbfade-1233-4922-9caa-aa8b0db9b3da"), Name = "Euston" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("15a2bb9a-4326-4a07-82ca-8393f037ebff"), Name = "South Hampstead" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("059ef56f-b255-4c0f-9815-26233878dd92"), Name = "Kilburn High Road" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("07f797b2-82bc-44c4-babf-52ed8cae31f1"), Name = "Queen's Park" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("ac87fec7-bea8-4c87-b09d-4e2826803d0d"), Name = "Kensal Green" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("d2323699-fac3-412d-af16-c4cd9da671f2"), Name = "Willesden Junction" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("e4a9c350-d6e4-41db-8d2d-5f9512ccf0fa"), Name = "Harlesden" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("2ccab4c9-a72c-4ac2-8d23-b81c75c28771"), Name = "Stonebridge Park" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("73fdbc30-94f4-49e4-b29c-62d9a378d602"), Name = "Wembley Central" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("9ffe4518-80c1-4e1d-87d1-1d84f5a18dfd"), Name = "North Wembley" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("22202e09-4136-434c-b3b3-44748c24b936"), Name = "South Kenton" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("30a08758-735a-4539-ab0e-6070bc0c4fd5"), Name = "Kenton" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("6be3e9ed-66af-4255-8910-d4b5857ba90a"), Name = "Harrow & Wealdstone" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("35d18731-7fa3-4714-9f67-af95e459c98a"), Name = "Headstone Lane" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("493bcdb5-010c-4762-ac81-d3e98448c60c"), Name = "Hatch End" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("7510c385-d474-439c-ac84-e65491cde2d2"), Name = "Carpenders Park" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("63bb23b1-0c4f-4361-a568-31d8d849ae2b"), Name = "Bushey" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("835bc6fb-ce32-4d92-8e12-2d0476cd014f"), Name = "Watford High Street" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("455d4830-bd0d-4962-8869-6443c4c8c452"), Name = "Watford Junction" });
    }

    [Fact]
    public void RouteRepository_Mildmay_Routes_Correct()
    {
        var result = _repository.Lines[Guid.Parse("73d9c9bd-fabd-49f7-9c67-dbb752f07453")];

        result.Name.Should().Be("Mildmay");
        result.ValidRoutes.Count.Should().Be(2);

        var resultStations = result.ValidRoutes[0];
        resultStations.Stations.Count.Should().Be(23);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("b02ebcb8-83a4-48e1-85d8-6e3fb21fa058"), Name = "Stratford" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("10c300fa-acc5-4701-8a3d-ba27a3327696"), Name = "Richmond" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("b02ebcb8-83a4-48e1-85d8-6e3fb21fa058"), Name = "Stratford" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("bd75d399-2ab3-4a0f-9f29-421853ce1f92"), Name = "Hackney Wick" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("d78c96d2-a714-4133-9362-ec1ab4f35435"), Name = "Homerton" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("aaa2565a-ba57-449b-b9b0-12bad4fb43a2"), Name = "Hackney Central" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("c88414e6-e599-42fa-be41-83ffaccc1108"), Name = "Dalston Kingsland" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("9c8db56b-4a81-4ae3-a5df-84ec7685ff82"), Name = "Canonbury" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("dd66ccf1-1f07-492f-b42f-67fd107889c2"), Name = "Highbury & Islington" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("a291b339-347c-484d-b743-3c17f83f1cac"), Name = "Caledonian Road & Barnsbury" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("21257a88-e690-4ef2-8a07-ef800b0ce07b"), Name = "Camden Road" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("1c9bf957-d6e8-47fd-93fb-f80379f87e4c"), Name = "Kentish Town West" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("79efd145-7794-40a0-a37a-f838223e641b"), Name = "Gospel Oak" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("621bf8be-ea0f-49d3-abcf-6db1153ff9aa"), Name = "Hampstead Heath" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("408d7020-ff90-46ae-8d7f-f74420057c30"), Name = "Finchley Road & Frognal" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("6b2099cd-f9f5-4d37-803c-82571d4fad6b"), Name = "West Hampstead" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("d57862c0-e7a7-4c7c-8c3e-d3d1c526aeb4"), Name = "Brondesbury" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("192e2b8a-31e5-4d90-8dc9-5d337c76e4b7"), Name = "Brondesbury Park" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("5d00d79a-1a33-4c4b-abfe-1dfe1c712685"), Name = "Kensal Rise" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("d2323699-fac3-412d-af16-c4cd9da671f2"), Name = "Willesden Junction" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("d2d12111-2070-4e0a-94c7-e4359d984f41"), Name = "Acton Central" });
        resultStations.Stations[19].Should().BeEquivalentTo(new { Id = Guid.Parse("b3ed3fba-5b92-4787-a0fb-302244e6ec30"), Name = "South Acton" });
        resultStations.Stations[20].Should().BeEquivalentTo(new { Id = Guid.Parse("4a1b2adb-a70e-42d1-b7e7-a2845ca98e7c"), Name = "Gunnersbury" });
        resultStations.Stations[21].Should().BeEquivalentTo(new { Id = Guid.Parse("92a0ee87-b829-4669-9482-fb8620455b4b"), Name = "Kew Gardens" });
        resultStations.Stations[22].Should().BeEquivalentTo(new { Id = Guid.Parse("10c300fa-acc5-4701-8a3d-ba27a3327696"), Name = "Richmond" });

        resultStations = result.ValidRoutes[1];
        resultStations.Stations.Count.Should().Be(23);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("b02ebcb8-83a4-48e1-85d8-6e3fb21fa058"), Name = "Stratford" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("bc1a08a6-11b7-49a4-955f-ba505cf2b555"), Name = "Clapham Junction" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("b02ebcb8-83a4-48e1-85d8-6e3fb21fa058"), Name = "Stratford" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("bd75d399-2ab3-4a0f-9f29-421853ce1f92"), Name = "Hackney Wick" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("d78c96d2-a714-4133-9362-ec1ab4f35435"), Name = "Homerton" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("aaa2565a-ba57-449b-b9b0-12bad4fb43a2"), Name = "Hackney Central" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("c88414e6-e599-42fa-be41-83ffaccc1108"), Name = "Dalston Kingsland" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("9c8db56b-4a81-4ae3-a5df-84ec7685ff82"), Name = "Canonbury" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("dd66ccf1-1f07-492f-b42f-67fd107889c2"), Name = "Highbury & Islington" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("a291b339-347c-484d-b743-3c17f83f1cac"), Name = "Caledonian Road & Barnsbury" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("21257a88-e690-4ef2-8a07-ef800b0ce07b"), Name = "Camden Road" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("1c9bf957-d6e8-47fd-93fb-f80379f87e4c"), Name = "Kentish Town West" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("79efd145-7794-40a0-a37a-f838223e641b"), Name = "Gospel Oak" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("621bf8be-ea0f-49d3-abcf-6db1153ff9aa"), Name = "Hampstead Heath" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("408d7020-ff90-46ae-8d7f-f74420057c30"), Name = "Finchley Road & Frognal" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("6b2099cd-f9f5-4d37-803c-82571d4fad6b"), Name = "West Hampstead" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("d57862c0-e7a7-4c7c-8c3e-d3d1c526aeb4"), Name = "Brondesbury" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("192e2b8a-31e5-4d90-8dc9-5d337c76e4b7"), Name = "Brondesbury Park" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("5d00d79a-1a33-4c4b-abfe-1dfe1c712685"), Name = "Kensal Rise" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("d2323699-fac3-412d-af16-c4cd9da671f2"), Name = "Willesden Junction" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("4c4529fd-1b39-4a34-afb2-b0c815151012"), Name = "Shepherd's Bush" });
        resultStations.Stations[19].Should().BeEquivalentTo(new { Id = Guid.Parse("b9a55e52-dbdc-4542-9981-8afe9e9709fe"), Name = "Kensington (Olympia)" });
        resultStations.Stations[20].Should().BeEquivalentTo(new { Id = Guid.Parse("6a65894f-41da-47e2-bfcc-68d877d2a9b2"), Name = "West Brompton" });
        resultStations.Stations[21].Should().BeEquivalentTo(new { Id = Guid.Parse("ca0b3456-d350-46c9-b17b-00c335425bd5"), Name = "Imperial Wharf" });
        resultStations.Stations[22].Should().BeEquivalentTo(new { Id = Guid.Parse("bc1a08a6-11b7-49a4-955f-ba505cf2b555"), Name = "Clapham Junction" });
    }

    [Fact]
    public void RouteRepository_Metropolitan_Routes_Correct()
    {
        var result = _repository.Lines[Guid.Parse("9e3a7f43-b6c4-4f12-9a72-ffbe2d15b9e6")];

        result.Name.Should().Be("Metropolitan");
        result.ValidRoutes.Count.Should().Be(4);

        var resultStations = result.ValidRoutes[0];
        resultStations.Stations.Count.Should().Be(24);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("b5fd3078-17d7-4e45-8ff6-d7d85025e1b0"), Name = "Amersham" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("2c5fbeab-ba64-4c8b-b21e-2336cdad37a5"), Name = "Aldgate" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("b5fd3078-17d7-4e45-8ff6-d7d85025e1b0"), Name = "Amersham" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("1dbfba04-272f-44f1-ad29-7961e0c0f327"), Name = "Chalfont & Latimer" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("983a6607-6b6b-4c3f-9888-7ef40d31ac07"), Name = "Chorleywood" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("271a1878-5415-4549-b63a-3e24e1118f89"), Name = "Rickmansworth" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("6187fce5-a122-4899-832a-1d33c616da94"), Name = "Moor Park" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("b38c1561-7a46-4a2c-a231-f07385f68cb9"), Name = "Northwood" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("cb591632-0af3-4682-a02c-b4c86e7729fa"), Name = "Northwood Hills" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("b1e8fe87-98d5-4d8a-bb64-229aaa23b834"), Name = "Pinner" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("fa7628ef-36bf-4464-8364-ee426748d217"), Name = "North Harrow" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("826ff626-0d02-4f1a-acd7-b6a30d142fbb"), Name = "Harrow On The Hill" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("b54eee79-e1e7-4bc7-b68e-e53b6c9436af"), Name = "Northwick Park" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("df4394a0-9357-4b11-9c6c-481bdab88eae"), Name = "Preston Road" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("5d1c8be3-f186-401e-a12c-b0d7ef0a6e3f"), Name = "Wembley Park" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("db9da180-7ba2-49d6-90c0-27bace8d6047"), Name = "Willesden Green" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("3a3c9204-f090-45fb-b3f5-774a8948248e"), Name = "Finchley Road" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("7d89b35f-9a87-49df-98ff-fd98f1f67235"), Name = "Baker Street" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("bd2ef776-2cea-49ad-80c4-a7e6587cc67b"), Name = "Great Portland Street" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("0ec876ed-6a2e-48e3-b8b0-6eb2ab13fc12"), Name = "Euston Square" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("5cf18e37-17e0-46c0-9177-6a5951df26b8"), Name = "King's Cross St.Pancras" });
        resultStations.Stations[19].Should().BeEquivalentTo(new { Id = Guid.Parse("bf6a3d1b-2af9-4b1c-b696-fc6f8c8cecde"), Name = "Farringdon" });
        resultStations.Stations[20].Should().BeEquivalentTo(new { Id = Guid.Parse("10972919-8db8-4a6c-aadf-ace0e43b5d8c"), Name = "Barbican" });
        resultStations.Stations[21].Should().BeEquivalentTo(new { Id = Guid.Parse("9343886e-5b86-4bbc-96bf-2fefe6240060"), Name = "Moorgate" });
        resultStations.Stations[22].Should().BeEquivalentTo(new { Id = Guid.Parse("db101bcd-350e-4485-b875-7ac2c8c1b6cc"), Name = "Liverpool Street" });
        resultStations.Stations[23].Should().BeEquivalentTo(new { Id = Guid.Parse("2c5fbeab-ba64-4c8b-b21e-2336cdad37a5"), Name = "Aldgate" });

        resultStations = result.ValidRoutes[1];
        resultStations.Stations.Count.Should().Be(23);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("a3f13d98-c40f-4ad1-9438-92345702ef7a"), Name = "Chesham" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("2c5fbeab-ba64-4c8b-b21e-2336cdad37a5"), Name = "Aldgate" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("a3f13d98-c40f-4ad1-9438-92345702ef7a"), Name = "Chesham" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("1dbfba04-272f-44f1-ad29-7961e0c0f327"), Name = "Chalfont & Latimer" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("983a6607-6b6b-4c3f-9888-7ef40d31ac07"), Name = "Chorleywood" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("271a1878-5415-4549-b63a-3e24e1118f89"), Name = "Rickmansworth" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("6187fce5-a122-4899-832a-1d33c616da94"), Name = "Moor Park" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("b38c1561-7a46-4a2c-a231-f07385f68cb9"), Name = "Northwood" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("cb591632-0af3-4682-a02c-b4c86e7729fa"), Name = "Northwood Hills" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("b1e8fe87-98d5-4d8a-bb64-229aaa23b834"), Name = "Pinner" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("fa7628ef-36bf-4464-8364-ee426748d217"), Name = "North Harrow" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("826ff626-0d02-4f1a-acd7-b6a30d142fbb"), Name = "Harrow On The Hill" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("b54eee79-e1e7-4bc7-b68e-e53b6c9436af"), Name = "Northwick Park" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("df4394a0-9357-4b11-9c6c-481bdab88eae"), Name = "Preston Road" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("5d1c8be3-f186-401e-a12c-b0d7ef0a6e3f"), Name = "Wembley Park" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("3a3c9204-f090-45fb-b3f5-774a8948248e"), Name = "Finchley Road" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("7d89b35f-9a87-49df-98ff-fd98f1f67235"), Name = "Baker Street" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("bd2ef776-2cea-49ad-80c4-a7e6587cc67b"), Name = "Great Portland Street" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("0ec876ed-6a2e-48e3-b8b0-6eb2ab13fc12"), Name = "Euston Square" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("5cf18e37-17e0-46c0-9177-6a5951df26b8"), Name = "King's Cross St.Pancras" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("bf6a3d1b-2af9-4b1c-b696-fc6f8c8cecde"), Name = "Farringdon" });
        resultStations.Stations[19].Should().BeEquivalentTo(new { Id = Guid.Parse("10972919-8db8-4a6c-aadf-ace0e43b5d8c"), Name = "Barbican" });
        resultStations.Stations[20].Should().BeEquivalentTo(new { Id = Guid.Parse("9343886e-5b86-4bbc-96bf-2fefe6240060"), Name = "Moorgate" });
        resultStations.Stations[21].Should().BeEquivalentTo(new { Id = Guid.Parse("db101bcd-350e-4485-b875-7ac2c8c1b6cc"), Name = "Liverpool Street" });
        resultStations.Stations[22].Should().BeEquivalentTo(new { Id = Guid.Parse("2c5fbeab-ba64-4c8b-b21e-2336cdad37a5"), Name = "Aldgate" });

        resultStations = result.ValidRoutes[2];
        resultStations.Stations.Count.Should().Be(22);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("28debada-e136-49a0-9143-9c0e5fb186e7"), Name = "Uxbridge" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("2c5fbeab-ba64-4c8b-b21e-2336cdad37a5"), Name = "Aldgate" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("28debada-e136-49a0-9143-9c0e5fb186e7"), Name = "Uxbridge" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("7f51c4eb-68de-4438-9682-7ad860db94e2"), Name = "Hillingdon" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("b00e11cc-848c-41c9-91db-b5b19304030e"), Name = "Ickenham" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("e5ec6361-2137-44ff-bb43-5154ebbbf037"), Name = "Ruislip" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("0b8eff14-c36f-4506-98f9-d3c88fbb50ad"), Name = "Ruislip Manor" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("fa21a248-fd28-4ff3-aea7-1c4b4aced4a0"), Name = "Eastcote" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("50c69ee2-8dfb-4e26-9477-3c860e36ca3e"), Name = "Rayners Lane" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("1ca02ad5-f21a-42e9-8541-3a39e12d4ea4"), Name = "West Harrow" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("826ff626-0d02-4f1a-acd7-b6a30d142fbb"), Name = "Harrow On The Hill" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("b54eee79-e1e7-4bc7-b68e-e53b6c9436af"), Name = "Northwick Park" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("df4394a0-9357-4b11-9c6c-481bdab88eae"), Name = "Preston Road" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("5d1c8be3-f186-401e-a12c-b0d7ef0a6e3f"), Name = "Wembley Park" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("3a3c9204-f090-45fb-b3f5-774a8948248e"), Name = "Finchley Road" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("7d89b35f-9a87-49df-98ff-fd98f1f67235"), Name = "Baker Street" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("bd2ef776-2cea-49ad-80c4-a7e6587cc67b"), Name = "Great Portland Street" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("0ec876ed-6a2e-48e3-b8b0-6eb2ab13fc12"), Name = "Euston Square" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("5cf18e37-17e0-46c0-9177-6a5951df26b8"), Name = "King's Cross St.Pancras" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("bf6a3d1b-2af9-4b1c-b696-fc6f8c8cecde"), Name = "Farringdon" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("10972919-8db8-4a6c-aadf-ace0e43b5d8c"), Name = "Barbican" });
        resultStations.Stations[19].Should().BeEquivalentTo(new { Id = Guid.Parse("9343886e-5b86-4bbc-96bf-2fefe6240060"), Name = "Moorgate" });
        resultStations.Stations[20].Should().BeEquivalentTo(new { Id = Guid.Parse("db101bcd-350e-4485-b875-7ac2c8c1b6cc"), Name = "Liverpool Street" });
        resultStations.Stations[21].Should().BeEquivalentTo(new { Id = Guid.Parse("2c5fbeab-ba64-4c8b-b21e-2336cdad37a5"), Name = "Aldgate" });

        resultStations = result.ValidRoutes[3];
        resultStations.Stations.Count.Should().Be(21);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("0d25ec4b-db42-4ca5-b2ac-8db28b79646e"), Name = "Watford" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("2c5fbeab-ba64-4c8b-b21e-2336cdad37a5"), Name = "Aldgate" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("0d25ec4b-db42-4ca5-b2ac-8db28b79646e"), Name = "Watford" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("3192ca0a-f6bb-4811-bf18-705027ad7a62"), Name = "Croxley" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("6187fce5-a122-4899-832a-1d33c616da94"), Name = "Moor Park" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("b38c1561-7a46-4a2c-a231-f07385f68cb9"), Name = "Northwood" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("cb591632-0af3-4682-a02c-b4c86e7729fa"), Name = "Northwood Hills" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("b1e8fe87-98d5-4d8a-bb64-229aaa23b834"), Name = "Pinner" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("fa7628ef-36bf-4464-8364-ee426748d217"), Name = "North Harrow" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("826ff626-0d02-4f1a-acd7-b6a30d142fbb"), Name = "Harrow On The Hill" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("b54eee79-e1e7-4bc7-b68e-e53b6c9436af"), Name = "Northwick Park" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("df4394a0-9357-4b11-9c6c-481bdab88eae"), Name = "Preston Road" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("5d1c8be3-f186-401e-a12c-b0d7ef0a6e3f"), Name = "Wembley Park" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("3a3c9204-f090-45fb-b3f5-774a8948248e"), Name = "Finchley Road" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("7d89b35f-9a87-49df-98ff-fd98f1f67235"), Name = "Baker Street" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("bd2ef776-2cea-49ad-80c4-a7e6587cc67b"), Name = "Great Portland Street" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("0ec876ed-6a2e-48e3-b8b0-6eb2ab13fc12"), Name = "Euston Square" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("5cf18e37-17e0-46c0-9177-6a5951df26b8"), Name = "King's Cross St.Pancras" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("bf6a3d1b-2af9-4b1c-b696-fc6f8c8cecde"), Name = "Farringdon" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("10972919-8db8-4a6c-aadf-ace0e43b5d8c"), Name = "Barbican" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("9343886e-5b86-4bbc-96bf-2fefe6240060"), Name = "Moorgate" });
        resultStations.Stations[19].Should().BeEquivalentTo(new { Id = Guid.Parse("db101bcd-350e-4485-b875-7ac2c8c1b6cc"), Name = "Liverpool Street" });
        resultStations.Stations[20].Should().BeEquivalentTo(new { Id = Guid.Parse("2c5fbeab-ba64-4c8b-b21e-2336cdad37a5"), Name = "Aldgate" });
    }

    [Fact]
    public void RouteRepository_Northen_Routes_Correct()
    {
        var result = _repository.Lines[Guid.Parse("62e93d5d-cc67-4c42-8ff5-24582f89d624")];

        result.Name.Should().Be("Northern");
        result.ValidRoutes.Count.Should().Be(8);

        var resultStations = result.ValidRoutes[0];
        resultStations.Stations.Count.Should().Be(22);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("668e4ceb-b98d-46d4-8d1f-6abb248fb577"), Name = "Battersea Power Station" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("bf7fcc36-2427-411c-81df-310175a4fbd1"), Name = "Edgware" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("668e4ceb-b98d-46d4-8d1f-6abb248fb577"), Name = "Battersea Power Station" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("c8d80c7d-5a93-44c7-8639-d26889567da8"), Name = "Nine Elms" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("846225f8-371b-4523-bcbb-fa12a4359d3b"), Name = "Kennington" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("8cebdb43-8d17-49a2-a06b-1f3513091845"), Name = "Waterloo" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("ae58d763-b367-4b09-9f1d-3be50467f47f"), Name = "Embankment" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("2dfc6d93-0d29-4525-9d64-2bcaaffe873b"), Name = "Charing Cross" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("f8fe3b87-f765-44f0-9604-efa8ecd324f6"), Name = "Leicester Square" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("70f156b3-520a-46e7-a35d-3f603c6ab5b7"), Name = "Tottenham Court Road" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("5ae7e630-0b7b-438b-bbd9-954dd3a7337a"), Name = "Goodge Street" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("e04078ef-639b-487e-b265-281db2f6b8a0"), Name = "Warren Street" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("cacbfade-1233-4922-9caa-aa8b0db9b3da"), Name = "Euston" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("ce569275-8972-4fc2-9562-31b375984aa1"), Name = "Mornington Crescent" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("a359263f-448b-42dd-a05f-660aa6ef53ec"), Name = "Camden Town" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("2b968966-119d-4ff2-97d2-77688b79aecb"), Name = "Chalk Farm" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("dc5cf8d4-9596-443f-9746-a2c98f8c1d68"), Name = "Belsize Park" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("2e67d28e-9e9c-455a-8102-3aeaca8d7587"), Name = "Hampstead" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("03d763a6-217a-4ca9-8c08-7a572902c8b3"), Name = "Golders Green" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("bb97079b-03a5-4c4b-8501-e3f2a22ac719"), Name = "Brent Cross" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("21a8fe74-dc2a-4360-b5e1-7b043a41bbaf"), Name = "Hendon Central" });
        resultStations.Stations[19].Should().BeEquivalentTo(new { Id = Guid.Parse("c4a502ac-6fd9-4f83-a802-b543ca99771f"), Name = "Colindale" });
        resultStations.Stations[20].Should().BeEquivalentTo(new { Id = Guid.Parse("ec8f48bc-23d5-4788-9251-f3fa1ff8a5d4"), Name = "Burnt Oak" });
        resultStations.Stations[21].Should().BeEquivalentTo(new { Id = Guid.Parse("bf7fcc36-2427-411c-81df-310175a4fbd1"), Name = "Edgware" });

        resultStations = result.ValidRoutes[1];
        resultStations.Stations.Count.Should().Be(23);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("668e4ceb-b98d-46d4-8d1f-6abb248fb577"), Name = "Battersea Power Station" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("a5e36b10-8d22-4a44-a0e2-e2f0a57e3c8b"), Name = "High Barnet" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("668e4ceb-b98d-46d4-8d1f-6abb248fb577"), Name = "Battersea Power Station" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("c8d80c7d-5a93-44c7-8639-d26889567da8"), Name = "Nine Elms" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("846225f8-371b-4523-bcbb-fa12a4359d3b"), Name = "Kennington" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("8cebdb43-8d17-49a2-a06b-1f3513091845"), Name = "Waterloo" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("ae58d763-b367-4b09-9f1d-3be50467f47f"), Name = "Embankment" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("2dfc6d93-0d29-4525-9d64-2bcaaffe873b"), Name = "Charing Cross" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("f8fe3b87-f765-44f0-9604-efa8ecd324f6"), Name = "Leicester Square" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("70f156b3-520a-46e7-a35d-3f603c6ab5b7"), Name = "Tottenham Court Road" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("5ae7e630-0b7b-438b-bbd9-954dd3a7337a"), Name = "Goodge Street" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("e04078ef-639b-487e-b265-281db2f6b8a0"), Name = "Warren Street" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("cacbfade-1233-4922-9caa-aa8b0db9b3da"), Name = "Euston" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("ce569275-8972-4fc2-9562-31b375984aa1"), Name = "Mornington Crescent" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("a359263f-448b-42dd-a05f-660aa6ef53ec"), Name = "Camden Town" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("c2691b4b-373f-459e-8349-4e9b67a60c16"), Name = "Kentish Town" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("37be4c8a-ff6f-4214-98fc-07c9ce4d1ba4"), Name = "Tufnell Park" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("386b3580-ea8b-4aed-a84b-a7aa205e24f1"), Name = "Archway" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("b8804bd7-0f43-4130-9726-9e146a7fa4b8"), Name = "Highgate" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("d33d8dfa-d56e-43c0-9400-7ffc46eb8633"), Name = "East Finchley" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("0be00e52-5c55-4419-bf9c-912b9c06c773"), Name = "Finchley Central" });
        resultStations.Stations[19].Should().BeEquivalentTo(new { Id = Guid.Parse("65b35caa-27b8-4c23-9cd2-d1854084da8a"), Name = "West Finchley" });
        resultStations.Stations[20].Should().BeEquivalentTo(new { Id = Guid.Parse("1b8ef572-2710-40b2-b211-52e4a4076020"), Name = "Woodside Park" });
        resultStations.Stations[21].Should().BeEquivalentTo(new { Id = Guid.Parse("75a4adff-12a4-49ab-b4c6-cf20b6a90987"), Name = "Totteridge & Whetstone" });
        resultStations.Stations[22].Should().BeEquivalentTo(new { Id = Guid.Parse("a5e36b10-8d22-4a44-a0e2-e2f0a57e3c8b"), Name = "High Barnet" });

        resultStations = result.ValidRoutes[2];
        resultStations.Stations.Count.Should().Be(31);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("215f94f9-f023-499b-a4e0-be95e4e0640b"), Name = "Morden" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("bf7fcc36-2427-411c-81df-310175a4fbd1"), Name = "Edgware" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("215f94f9-f023-499b-a4e0-be95e4e0640b"), Name = "Morden" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("dbd2caf6-6081-4733-a57e-8bcbc327cd6b"), Name = "South Wimbledon" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("d338915d-7662-445b-ba08-cf57b973d9a7"), Name = "Colliers Wood" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("70433cf5-d068-4c96-9f23-2fe2cff2c835"), Name = "Tooting Broadway" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("9da27c2a-8ab6-4b31-be6f-9e6f4e0f3667"), Name = "Tooting Bec" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("5aa36340-7ebe-4265-b7e2-679ca385cb96"), Name = "Balham" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("6be556ff-17e8-496e-93ba-85f493668245"), Name = "Clapham South" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("843a1e32-7aea-49e0-8e51-e57dfb0d13ce"), Name = "Clapham Common" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("fcb20f8d-2a68-4179-9831-629b8fc6894e"), Name = "Clapham North" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("e7beb5c1-a574-421f-b92e-ea66acddc230"), Name = "Stockwell" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("091e449a-b0d9-45fe-a2c6-c15eaa8dbd52"), Name = "Oval" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("846225f8-371b-4523-bcbb-fa12a4359d3b"), Name = "Kennington" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("a3c4de46-14f3-420c-8f55-c94e44f3b079"), Name = "Elephant & Castle" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("3306ba28-b7c8-4325-991b-f83fd02b1267"), Name = "Borough" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("9c8c4a97-c895-4c03-bba7-0f54a3b11bb3"), Name = "London Bridge" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("aaedc653-e766-4d6b-87e2-4c87322971ef"), Name = "Bank" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("9343886e-5b86-4bbc-96bf-2fefe6240060"), Name = "Moorgate" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("e0f260e2-0cf7-40dd-ba35-21aff58b721a"), Name = "Old Street" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("57fd1550-4c55-434e-8e96-041207c1ac63"), Name = "Angel" });
        resultStations.Stations[19].Should().BeEquivalentTo(new { Id = Guid.Parse("5cf18e37-17e0-46c0-9177-6a5951df26b8"), Name = "King's Cross St.Pancras" });
        resultStations.Stations[20].Should().BeEquivalentTo(new { Id = Guid.Parse("cacbfade-1233-4922-9caa-aa8b0db9b3da"), Name = "Euston" });
        resultStations.Stations[21].Should().BeEquivalentTo(new { Id = Guid.Parse("a359263f-448b-42dd-a05f-660aa6ef53ec"), Name = "Camden Town" });
        resultStations.Stations[22].Should().BeEquivalentTo(new { Id = Guid.Parse("2b968966-119d-4ff2-97d2-77688b79aecb"), Name = "Chalk Farm" });
        resultStations.Stations[23].Should().BeEquivalentTo(new { Id = Guid.Parse("dc5cf8d4-9596-443f-9746-a2c98f8c1d68"), Name = "Belsize Park" });
        resultStations.Stations[24].Should().BeEquivalentTo(new { Id = Guid.Parse("2e67d28e-9e9c-455a-8102-3aeaca8d7587"), Name = "Hampstead" });
        resultStations.Stations[25].Should().BeEquivalentTo(new { Id = Guid.Parse("03d763a6-217a-4ca9-8c08-7a572902c8b3"), Name = "Golders Green" });
        resultStations.Stations[26].Should().BeEquivalentTo(new { Id = Guid.Parse("bb97079b-03a5-4c4b-8501-e3f2a22ac719"), Name = "Brent Cross" });
        resultStations.Stations[27].Should().BeEquivalentTo(new { Id = Guid.Parse("21a8fe74-dc2a-4360-b5e1-7b043a41bbaf"), Name = "Hendon Central" });
        resultStations.Stations[28].Should().BeEquivalentTo(new { Id = Guid.Parse("c4a502ac-6fd9-4f83-a802-b543ca99771f"), Name = "Colindale" });
        resultStations.Stations[29].Should().BeEquivalentTo(new { Id = Guid.Parse("ec8f48bc-23d5-4788-9251-f3fa1ff8a5d4"), Name = "Burnt Oak" });
        resultStations.Stations[30].Should().BeEquivalentTo(new { Id = Guid.Parse("bf7fcc36-2427-411c-81df-310175a4fbd1"), Name = "Edgware" });

        resultStations = result.ValidRoutes[3];
        resultStations.Stations.Count.Should().Be(29);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("215f94f9-f023-499b-a4e0-be95e4e0640b"), Name = "Morden" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("38c3603c-2eb6-4916-9fbd-c01d91a50259"), Name = "Mill Hill East" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("215f94f9-f023-499b-a4e0-be95e4e0640b"), Name = "Morden" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("dbd2caf6-6081-4733-a57e-8bcbc327cd6b"), Name = "South Wimbledon" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("d338915d-7662-445b-ba08-cf57b973d9a7"), Name = "Colliers Wood" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("70433cf5-d068-4c96-9f23-2fe2cff2c835"), Name = "Tooting Broadway" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("9da27c2a-8ab6-4b31-be6f-9e6f4e0f3667"), Name = "Tooting Bec" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("5aa36340-7ebe-4265-b7e2-679ca385cb96"), Name = "Balham" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("6be556ff-17e8-496e-93ba-85f493668245"), Name = "Clapham South" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("843a1e32-7aea-49e0-8e51-e57dfb0d13ce"), Name = "Clapham Common" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("fcb20f8d-2a68-4179-9831-629b8fc6894e"), Name = "Clapham North" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("e7beb5c1-a574-421f-b92e-ea66acddc230"), Name = "Stockwell" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("091e449a-b0d9-45fe-a2c6-c15eaa8dbd52"), Name = "Oval" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("846225f8-371b-4523-bcbb-fa12a4359d3b"), Name = "Kennington" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("a3c4de46-14f3-420c-8f55-c94e44f3b079"), Name = "Elephant & Castle" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("3306ba28-b7c8-4325-991b-f83fd02b1267"), Name = "Borough" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("9c8c4a97-c895-4c03-bba7-0f54a3b11bb3"), Name = "London Bridge" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("aaedc653-e766-4d6b-87e2-4c87322971ef"), Name = "Bank" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("9343886e-5b86-4bbc-96bf-2fefe6240060"), Name = "Moorgate" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("e0f260e2-0cf7-40dd-ba35-21aff58b721a"), Name = "Old Street" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("57fd1550-4c55-434e-8e96-041207c1ac63"), Name = "Angel" });
        resultStations.Stations[19].Should().BeEquivalentTo(new { Id = Guid.Parse("5cf18e37-17e0-46c0-9177-6a5951df26b8"), Name = "King's Cross St.Pancras" });
        resultStations.Stations[20].Should().BeEquivalentTo(new { Id = Guid.Parse("cacbfade-1233-4922-9caa-aa8b0db9b3da"), Name = "Euston" });
        resultStations.Stations[21].Should().BeEquivalentTo(new { Id = Guid.Parse("a359263f-448b-42dd-a05f-660aa6ef53ec"), Name = "Camden Town" });
        resultStations.Stations[22].Should().BeEquivalentTo(new { Id = Guid.Parse("c2691b4b-373f-459e-8349-4e9b67a60c16"), Name = "Kentish Town" });
        resultStations.Stations[23].Should().BeEquivalentTo(new { Id = Guid.Parse("37be4c8a-ff6f-4214-98fc-07c9ce4d1ba4"), Name = "Tufnell Park" });
        resultStations.Stations[24].Should().BeEquivalentTo(new { Id = Guid.Parse("386b3580-ea8b-4aed-a84b-a7aa205e24f1"), Name = "Archway" });
        resultStations.Stations[25].Should().BeEquivalentTo(new { Id = Guid.Parse("b8804bd7-0f43-4130-9726-9e146a7fa4b8"), Name = "Highgate" });
        resultStations.Stations[26].Should().BeEquivalentTo(new { Id = Guid.Parse("d33d8dfa-d56e-43c0-9400-7ffc46eb8633"), Name = "East Finchley" });
        resultStations.Stations[27].Should().BeEquivalentTo(new { Id = Guid.Parse("0be00e52-5c55-4419-bf9c-912b9c06c773"), Name = "Finchley Central" });
        resultStations.Stations[28].Should().BeEquivalentTo(new { Id = Guid.Parse("38c3603c-2eb6-4916-9fbd-c01d91a50259"), Name = "Mill Hill East" });

        resultStations = result.ValidRoutes[4];
        resultStations.Stations.Count.Should().Be(32);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("215f94f9-f023-499b-a4e0-be95e4e0640b"), Name = "Morden" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("a5e36b10-8d22-4a44-a0e2-e2f0a57e3c8b"), Name = "High Barnet" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("215f94f9-f023-499b-a4e0-be95e4e0640b"), Name = "Morden" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("dbd2caf6-6081-4733-a57e-8bcbc327cd6b"), Name = "South Wimbledon" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("d338915d-7662-445b-ba08-cf57b973d9a7"), Name = "Colliers Wood" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("70433cf5-d068-4c96-9f23-2fe2cff2c835"), Name = "Tooting Broadway" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("9da27c2a-8ab6-4b31-be6f-9e6f4e0f3667"), Name = "Tooting Bec" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("5aa36340-7ebe-4265-b7e2-679ca385cb96"), Name = "Balham" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("6be556ff-17e8-496e-93ba-85f493668245"), Name = "Clapham South" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("843a1e32-7aea-49e0-8e51-e57dfb0d13ce"), Name = "Clapham Common" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("fcb20f8d-2a68-4179-9831-629b8fc6894e"), Name = "Clapham North" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("e7beb5c1-a574-421f-b92e-ea66acddc230"), Name = "Stockwell" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("091e449a-b0d9-45fe-a2c6-c15eaa8dbd52"), Name = "Oval" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("846225f8-371b-4523-bcbb-fa12a4359d3b"), Name = "Kennington" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("a3c4de46-14f3-420c-8f55-c94e44f3b079"), Name = "Elephant & Castle" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("3306ba28-b7c8-4325-991b-f83fd02b1267"), Name = "Borough" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("9c8c4a97-c895-4c03-bba7-0f54a3b11bb3"), Name = "London Bridge" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("aaedc653-e766-4d6b-87e2-4c87322971ef"), Name = "Bank" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("9343886e-5b86-4bbc-96bf-2fefe6240060"), Name = "Moorgate" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("e0f260e2-0cf7-40dd-ba35-21aff58b721a"), Name = "Old Street" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("57fd1550-4c55-434e-8e96-041207c1ac63"), Name = "Angel" });
        resultStations.Stations[19].Should().BeEquivalentTo(new { Id = Guid.Parse("5cf18e37-17e0-46c0-9177-6a5951df26b8"), Name = "King's Cross St.Pancras" });
        resultStations.Stations[20].Should().BeEquivalentTo(new { Id = Guid.Parse("cacbfade-1233-4922-9caa-aa8b0db9b3da"), Name = "Euston" });
        resultStations.Stations[21].Should().BeEquivalentTo(new { Id = Guid.Parse("a359263f-448b-42dd-a05f-660aa6ef53ec"), Name = "Camden Town" });
        resultStations.Stations[22].Should().BeEquivalentTo(new { Id = Guid.Parse("c2691b4b-373f-459e-8349-4e9b67a60c16"), Name = "Kentish Town" });
        resultStations.Stations[23].Should().BeEquivalentTo(new { Id = Guid.Parse("37be4c8a-ff6f-4214-98fc-07c9ce4d1ba4"), Name = "Tufnell Park" });
        resultStations.Stations[24].Should().BeEquivalentTo(new { Id = Guid.Parse("386b3580-ea8b-4aed-a84b-a7aa205e24f1"), Name = "Archway" });
        resultStations.Stations[25].Should().BeEquivalentTo(new { Id = Guid.Parse("b8804bd7-0f43-4130-9726-9e146a7fa4b8"), Name = "Highgate" });
        resultStations.Stations[26].Should().BeEquivalentTo(new { Id = Guid.Parse("d33d8dfa-d56e-43c0-9400-7ffc46eb8633"), Name = "East Finchley" });
        resultStations.Stations[27].Should().BeEquivalentTo(new { Id = Guid.Parse("0be00e52-5c55-4419-bf9c-912b9c06c773"), Name = "Finchley Central" });
        resultStations.Stations[28].Should().BeEquivalentTo(new { Id = Guid.Parse("65b35caa-27b8-4c23-9cd2-d1854084da8a"), Name = "West Finchley" });
        resultStations.Stations[29].Should().BeEquivalentTo(new { Id = Guid.Parse("1b8ef572-2710-40b2-b211-52e4a4076020"), Name = "Woodside Park" });
        resultStations.Stations[30].Should().BeEquivalentTo(new { Id = Guid.Parse("75a4adff-12a4-49ab-b4c6-cf20b6a90987"), Name = "Totteridge & Whetstone" });
        resultStations.Stations[31].Should().BeEquivalentTo(new { Id = Guid.Parse("a5e36b10-8d22-4a44-a0e2-e2f0a57e3c8b"), Name = "High Barnet" });

        resultStations = result.ValidRoutes[5];
        resultStations.Stations.Count.Should().Be(31);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("215f94f9-f023-499b-a4e0-be95e4e0640b"), Name = "Morden" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("bf7fcc36-2427-411c-81df-310175a4fbd1"), Name = "Edgware" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("215f94f9-f023-499b-a4e0-be95e4e0640b"), Name = "Morden" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("dbd2caf6-6081-4733-a57e-8bcbc327cd6b"), Name = "South Wimbledon" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("d338915d-7662-445b-ba08-cf57b973d9a7"), Name = "Colliers Wood" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("70433cf5-d068-4c96-9f23-2fe2cff2c835"), Name = "Tooting Broadway" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("9da27c2a-8ab6-4b31-be6f-9e6f4e0f3667"), Name = "Tooting Bec" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("5aa36340-7ebe-4265-b7e2-679ca385cb96"), Name = "Balham" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("6be556ff-17e8-496e-93ba-85f493668245"), Name = "Clapham South" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("843a1e32-7aea-49e0-8e51-e57dfb0d13ce"), Name = "Clapham Common" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("fcb20f8d-2a68-4179-9831-629b8fc6894e"), Name = "Clapham North" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("e7beb5c1-a574-421f-b92e-ea66acddc230"), Name = "Stockwell" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("091e449a-b0d9-45fe-a2c6-c15eaa8dbd52"), Name = "Oval" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("846225f8-371b-4523-bcbb-fa12a4359d3b"), Name = "Kennington" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("8cebdb43-8d17-49a2-a06b-1f3513091845"), Name = "Waterloo" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("ae58d763-b367-4b09-9f1d-3be50467f47f"), Name = "Embankment" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("2dfc6d93-0d29-4525-9d64-2bcaaffe873b"), Name = "Charing Cross" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("f8fe3b87-f765-44f0-9604-efa8ecd324f6"), Name = "Leicester Square" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("70f156b3-520a-46e7-a35d-3f603c6ab5b7"), Name = "Tottenham Court Road" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("5ae7e630-0b7b-438b-bbd9-954dd3a7337a"), Name = "Goodge Street" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("e04078ef-639b-487e-b265-281db2f6b8a0"), Name = "Warren Street" });
        resultStations.Stations[19].Should().BeEquivalentTo(new { Id = Guid.Parse("cacbfade-1233-4922-9caa-aa8b0db9b3da"), Name = "Euston" });
        resultStations.Stations[20].Should().BeEquivalentTo(new { Id = Guid.Parse("ce569275-8972-4fc2-9562-31b375984aa1"), Name = "Mornington Crescent" });
        resultStations.Stations[21].Should().BeEquivalentTo(new { Id = Guid.Parse("a359263f-448b-42dd-a05f-660aa6ef53ec"), Name = "Camden Town" });
        resultStations.Stations[22].Should().BeEquivalentTo(new { Id = Guid.Parse("2b968966-119d-4ff2-97d2-77688b79aecb"), Name = "Chalk Farm" });
        resultStations.Stations[23].Should().BeEquivalentTo(new { Id = Guid.Parse("dc5cf8d4-9596-443f-9746-a2c98f8c1d68"), Name = "Belsize Park" });
        resultStations.Stations[24].Should().BeEquivalentTo(new { Id = Guid.Parse("2e67d28e-9e9c-455a-8102-3aeaca8d7587"), Name = "Hampstead" });
        resultStations.Stations[25].Should().BeEquivalentTo(new { Id = Guid.Parse("03d763a6-217a-4ca9-8c08-7a572902c8b3"), Name = "Golders Green" });
        resultStations.Stations[26].Should().BeEquivalentTo(new { Id = Guid.Parse("bb97079b-03a5-4c4b-8501-e3f2a22ac719"), Name = "Brent Cross" });
        resultStations.Stations[27].Should().BeEquivalentTo(new { Id = Guid.Parse("21a8fe74-dc2a-4360-b5e1-7b043a41bbaf"), Name = "Hendon Central" });
        resultStations.Stations[28].Should().BeEquivalentTo(new { Id = Guid.Parse("c4a502ac-6fd9-4f83-a802-b543ca99771f"), Name = "Colindale" });
        resultStations.Stations[29].Should().BeEquivalentTo(new { Id = Guid.Parse("ec8f48bc-23d5-4788-9251-f3fa1ff8a5d4"), Name = "Burnt Oak" });
        resultStations.Stations[30].Should().BeEquivalentTo(new { Id = Guid.Parse("bf7fcc36-2427-411c-81df-310175a4fbd1"), Name = "Edgware" });

        resultStations = result.ValidRoutes[6];
        resultStations.Stations.Count.Should().Be(29);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("215f94f9-f023-499b-a4e0-be95e4e0640b"), Name = "Morden" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("38c3603c-2eb6-4916-9fbd-c01d91a50259"), Name = "Mill Hill East" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("215f94f9-f023-499b-a4e0-be95e4e0640b"), Name = "Morden" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("dbd2caf6-6081-4733-a57e-8bcbc327cd6b"), Name = "South Wimbledon" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("d338915d-7662-445b-ba08-cf57b973d9a7"), Name = "Colliers Wood" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("70433cf5-d068-4c96-9f23-2fe2cff2c835"), Name = "Tooting Broadway" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("9da27c2a-8ab6-4b31-be6f-9e6f4e0f3667"), Name = "Tooting Bec" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("5aa36340-7ebe-4265-b7e2-679ca385cb96"), Name = "Balham" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("6be556ff-17e8-496e-93ba-85f493668245"), Name = "Clapham South" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("843a1e32-7aea-49e0-8e51-e57dfb0d13ce"), Name = "Clapham Common" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("fcb20f8d-2a68-4179-9831-629b8fc6894e"), Name = "Clapham North" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("e7beb5c1-a574-421f-b92e-ea66acddc230"), Name = "Stockwell" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("091e449a-b0d9-45fe-a2c6-c15eaa8dbd52"), Name = "Oval" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("846225f8-371b-4523-bcbb-fa12a4359d3b"), Name = "Kennington" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("8cebdb43-8d17-49a2-a06b-1f3513091845"), Name = "Waterloo" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("ae58d763-b367-4b09-9f1d-3be50467f47f"), Name = "Embankment" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("2dfc6d93-0d29-4525-9d64-2bcaaffe873b"), Name = "Charing Cross" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("f8fe3b87-f765-44f0-9604-efa8ecd324f6"), Name = "Leicester Square" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("70f156b3-520a-46e7-a35d-3f603c6ab5b7"), Name = "Tottenham Court Road" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("5ae7e630-0b7b-438b-bbd9-954dd3a7337a"), Name = "Goodge Street" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("e04078ef-639b-487e-b265-281db2f6b8a0"), Name = "Warren Street" });
        resultStations.Stations[19].Should().BeEquivalentTo(new { Id = Guid.Parse("cacbfade-1233-4922-9caa-aa8b0db9b3da"), Name = "Euston" });
        resultStations.Stations[20].Should().BeEquivalentTo(new { Id = Guid.Parse("ce569275-8972-4fc2-9562-31b375984aa1"), Name = "Mornington Crescent" });
        resultStations.Stations[21].Should().BeEquivalentTo(new { Id = Guid.Parse("a359263f-448b-42dd-a05f-660aa6ef53ec"), Name = "Camden Town" });
        resultStations.Stations[22].Should().BeEquivalentTo(new { Id = Guid.Parse("c2691b4b-373f-459e-8349-4e9b67a60c16"), Name = "Kentish Town" });
        resultStations.Stations[23].Should().BeEquivalentTo(new { Id = Guid.Parse("37be4c8a-ff6f-4214-98fc-07c9ce4d1ba4"), Name = "Tufnell Park" });
        resultStations.Stations[24].Should().BeEquivalentTo(new { Id = Guid.Parse("386b3580-ea8b-4aed-a84b-a7aa205e24f1"), Name = "Archway" });
        resultStations.Stations[25].Should().BeEquivalentTo(new { Id = Guid.Parse("b8804bd7-0f43-4130-9726-9e146a7fa4b8"), Name = "Highgate" });
        resultStations.Stations[26].Should().BeEquivalentTo(new { Id = Guid.Parse("d33d8dfa-d56e-43c0-9400-7ffc46eb8633"), Name = "East Finchley" });
        resultStations.Stations[27].Should().BeEquivalentTo(new { Id = Guid.Parse("0be00e52-5c55-4419-bf9c-912b9c06c773"), Name = "Finchley Central" });
        resultStations.Stations[28].Should().BeEquivalentTo(new { Id = Guid.Parse("38c3603c-2eb6-4916-9fbd-c01d91a50259"), Name = "Mill Hill East" });

        resultStations = result.ValidRoutes[7];
        resultStations.Stations.Count.Should().Be(32);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("215f94f9-f023-499b-a4e0-be95e4e0640b"), Name = "Morden" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("a5e36b10-8d22-4a44-a0e2-e2f0a57e3c8b"), Name = "High Barnet" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("215f94f9-f023-499b-a4e0-be95e4e0640b"), Name = "Morden" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("dbd2caf6-6081-4733-a57e-8bcbc327cd6b"), Name = "South Wimbledon" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("d338915d-7662-445b-ba08-cf57b973d9a7"), Name = "Colliers Wood" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("70433cf5-d068-4c96-9f23-2fe2cff2c835"), Name = "Tooting Broadway" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("9da27c2a-8ab6-4b31-be6f-9e6f4e0f3667"), Name = "Tooting Bec" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("5aa36340-7ebe-4265-b7e2-679ca385cb96"), Name = "Balham" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("6be556ff-17e8-496e-93ba-85f493668245"), Name = "Clapham South" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("843a1e32-7aea-49e0-8e51-e57dfb0d13ce"), Name = "Clapham Common" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("fcb20f8d-2a68-4179-9831-629b8fc6894e"), Name = "Clapham North" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("e7beb5c1-a574-421f-b92e-ea66acddc230"), Name = "Stockwell" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("091e449a-b0d9-45fe-a2c6-c15eaa8dbd52"), Name = "Oval" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("846225f8-371b-4523-bcbb-fa12a4359d3b"), Name = "Kennington" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("8cebdb43-8d17-49a2-a06b-1f3513091845"), Name = "Waterloo" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("ae58d763-b367-4b09-9f1d-3be50467f47f"), Name = "Embankment" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("2dfc6d93-0d29-4525-9d64-2bcaaffe873b"), Name = "Charing Cross" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("f8fe3b87-f765-44f0-9604-efa8ecd324f6"), Name = "Leicester Square" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("70f156b3-520a-46e7-a35d-3f603c6ab5b7"), Name = "Tottenham Court Road" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("5ae7e630-0b7b-438b-bbd9-954dd3a7337a"), Name = "Goodge Street" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("e04078ef-639b-487e-b265-281db2f6b8a0"), Name = "Warren Street" });
        resultStations.Stations[19].Should().BeEquivalentTo(new { Id = Guid.Parse("cacbfade-1233-4922-9caa-aa8b0db9b3da"), Name = "Euston" });
        resultStations.Stations[20].Should().BeEquivalentTo(new { Id = Guid.Parse("ce569275-8972-4fc2-9562-31b375984aa1"), Name = "Mornington Crescent" });
        resultStations.Stations[21].Should().BeEquivalentTo(new { Id = Guid.Parse("a359263f-448b-42dd-a05f-660aa6ef53ec"), Name = "Camden Town" });
        resultStations.Stations[22].Should().BeEquivalentTo(new { Id = Guid.Parse("c2691b4b-373f-459e-8349-4e9b67a60c16"), Name = "Kentish Town" });
        resultStations.Stations[23].Should().BeEquivalentTo(new { Id = Guid.Parse("37be4c8a-ff6f-4214-98fc-07c9ce4d1ba4"), Name = "Tufnell Park" });
        resultStations.Stations[24].Should().BeEquivalentTo(new { Id = Guid.Parse("386b3580-ea8b-4aed-a84b-a7aa205e24f1"), Name = "Archway" });
        resultStations.Stations[25].Should().BeEquivalentTo(new { Id = Guid.Parse("b8804bd7-0f43-4130-9726-9e146a7fa4b8"), Name = "Highgate" });
        resultStations.Stations[26].Should().BeEquivalentTo(new { Id = Guid.Parse("d33d8dfa-d56e-43c0-9400-7ffc46eb8633"), Name = "East Finchley" });
        resultStations.Stations[27].Should().BeEquivalentTo(new { Id = Guid.Parse("0be00e52-5c55-4419-bf9c-912b9c06c773"), Name = "Finchley Central" });
        resultStations.Stations[28].Should().BeEquivalentTo(new { Id = Guid.Parse("65b35caa-27b8-4c23-9cd2-d1854084da8a"), Name = "West Finchley" });
        resultStations.Stations[29].Should().BeEquivalentTo(new { Id = Guid.Parse("1b8ef572-2710-40b2-b211-52e4a4076020"), Name = "Woodside Park" });
        resultStations.Stations[30].Should().BeEquivalentTo(new { Id = Guid.Parse("75a4adff-12a4-49ab-b4c6-cf20b6a90987"), Name = "Totteridge & Whetstone" });
        resultStations.Stations[31].Should().BeEquivalentTo(new { Id = Guid.Parse("a5e36b10-8d22-4a44-a0e2-e2f0a57e3c8b"), Name = "High Barnet" });
    }

    [Fact]
    public void RouteRepository_Piccadilly_Routes_Correct()
    {
        var result = _repository.Lines[Guid.Parse("6c9e1d2c-845e-4d08-885f-17b9cf28e154")];

        result.Name.Should().Be("Piccadilly");
        result.ValidRoutes.Count.Should().Be(3);

        var resultStations = result.ValidRoutes[0];
        resultStations.Stations.Count.Should().Be(38);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("a7ce86a8-d1a2-4c40-a8f0-0ead55233d5d"), Name = "Heathrow Terminal 4" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("392e1c83-1915-43ea-9685-aff7a8aa4bb5"), Name = "Cockfosters" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("a7ce86a8-d1a2-4c40-a8f0-0ead55233d5d"), Name = "Heathrow Terminal 4" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("d4ecc02a-082d-4408-9f4f-2e4be6655681"), Name = "Heathrow Terminals 2 & 3" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("343bafce-1f7b-4fc7-b07e-1bfc5d4b547f"), Name = "Hatton Cross" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("9c955dab-5375-495e-8426-2e95e8ede204"), Name = "Hounslow West" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("b8e4a0cd-382f-41f0-974e-eb6fd55fabd3"), Name = "Hounslow Central" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("03cafb34-fd9d-4069-ae3d-b61e83b53da6"), Name = "Hounslow East" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("75630ba5-3767-44da-b14a-5cb67caa1875"), Name = "Osterley" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("8f1b0a2d-93ff-41d2-bcce-7685972b83ba"), Name = "Boston Manor" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("8542a84b-b9a8-4937-a910-1e0b5f778377"), Name = "Northfields" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("8a1409d6-217c-4024-8494-c34689a89a6e"), Name = "South Ealing" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("82a3d734-abf8-43f1-853d-a69f938c20b1"), Name = "Acton Town" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("612f4d89-3086-4e1b-9771-d0e748718eb8"), Name = "Turnham Green" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("bdf7bc22-9e0b-4bfc-8abe-8130f5c462c8"), Name = "Hammersmith" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("6ea41ee3-5e14-4830-af0a-02d2bd9bcf98"), Name = "Barons Court" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("03c1cead-6d76-40f7-b67d-0eecef00220b"), Name = "Earl's Court" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("94b2e2cc-4150-4ab0-b387-21e2a13960e1"), Name = "Gloucester Road" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("4afcf7aa-f5ee-499f-9484-38e0b0c0af0b"), Name = "South Kensington" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("dc662ccf-3be3-40bb-ae03-4534bbaafe19"), Name = "Knightsbridge" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("a4965d09-d0fa-49f7-90bd-d550eb713670"), Name = "Hyde Park Corner" });
        resultStations.Stations[19].Should().BeEquivalentTo(new { Id = Guid.Parse("a88f9ee1-e742-44ea-96b1-467df4a561a2"), Name = "Green Park" });
        resultStations.Stations[20].Should().BeEquivalentTo(new { Id = Guid.Parse("da1c4ff7-2831-48f0-a7a2-112a03c68d37"), Name = "Piccadilly Circus" });
        resultStations.Stations[21].Should().BeEquivalentTo(new { Id = Guid.Parse("f8fe3b87-f765-44f0-9604-efa8ecd324f6"), Name = "Leicester Square" });
        resultStations.Stations[22].Should().BeEquivalentTo(new { Id = Guid.Parse("ad7af5d5-75be-4f42-9ea1-5191046ef6f8"), Name = "Covent Garden" });
        resultStations.Stations[23].Should().BeEquivalentTo(new { Id = Guid.Parse("92fef3de-c4db-47d2-ac5c-a188c6ab604c"), Name = "Holborn" });
        resultStations.Stations[24].Should().BeEquivalentTo(new { Id = Guid.Parse("361ec66d-3aed-4df6-aab6-b7fe955323b4"), Name = "Russell Square" });
        resultStations.Stations[25].Should().BeEquivalentTo(new { Id = Guid.Parse("5cf18e37-17e0-46c0-9177-6a5951df26b8"), Name = "King's Cross St.Pancras" });
        resultStations.Stations[26].Should().BeEquivalentTo(new { Id = Guid.Parse("36ce6d95-4979-4511-aef0-aa8f7b031838"), Name = "Caledonian Road" });
        resultStations.Stations[27].Should().BeEquivalentTo(new { Id = Guid.Parse("d39cb0f5-1858-41b7-b477-ade85012bb64"), Name = "Holloway Road" });
        resultStations.Stations[28].Should().BeEquivalentTo(new { Id = Guid.Parse("09ba8371-b0f3-431e-b549-8af06b991af0"), Name = "Arsenal" });
        resultStations.Stations[29].Should().BeEquivalentTo(new { Id = Guid.Parse("bc3918ec-947f-489e-aa05-3e2272232c78"), Name = "Finsbury Park" });
        resultStations.Stations[30].Should().BeEquivalentTo(new { Id = Guid.Parse("7dccb6db-2786-438a-b151-c5eaccdb1f9e"), Name = "Manor House" });
        resultStations.Stations[31].Should().BeEquivalentTo(new { Id = Guid.Parse("7ba4edc7-7177-4031-8f23-7f92bcc043fc"), Name = "Turnpike Lane" });
        resultStations.Stations[32].Should().BeEquivalentTo(new { Id = Guid.Parse("04fe0617-859e-457c-b8f3-42d050640d5e"), Name = "Wood Green" });
        resultStations.Stations[33].Should().BeEquivalentTo(new { Id = Guid.Parse("5ec78131-73b3-4b18-8e2f-d1b71eaa63d7"), Name = "Bounds Green" });
        resultStations.Stations[34].Should().BeEquivalentTo(new { Id = Guid.Parse("bb11562c-6862-43fa-b5b7-678a80d20032"), Name = "Arnos Grove" });
        resultStations.Stations[35].Should().BeEquivalentTo(new { Id = Guid.Parse("98ea0b91-b16b-4423-9e4b-1df0891019ea"), Name = "Southgate" });
        resultStations.Stations[36].Should().BeEquivalentTo(new { Id = Guid.Parse("68b4658a-683c-49b6-b9e1-ab5739f42d24"), Name = "Oakwood" });
        resultStations.Stations[37].Should().BeEquivalentTo(new { Id = Guid.Parse("392e1c83-1915-43ea-9685-aff7a8aa4bb5"), Name = "Cockfosters" });

        resultStations = result.ValidRoutes[1];
        resultStations.Stations.Count.Should().Be(38);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("7c583322-8051-4e43-a533-e372bdb3f875"), Name = "Heathrow Terminal 5" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("392e1c83-1915-43ea-9685-aff7a8aa4bb5"), Name = "Cockfosters" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("7c583322-8051-4e43-a533-e372bdb3f875"), Name = "Heathrow Terminal 5" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("d4ecc02a-082d-4408-9f4f-2e4be6655681"), Name = "Heathrow Terminals 2 & 3" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("343bafce-1f7b-4fc7-b07e-1bfc5d4b547f"), Name = "Hatton Cross" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("9c955dab-5375-495e-8426-2e95e8ede204"), Name = "Hounslow West" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("b8e4a0cd-382f-41f0-974e-eb6fd55fabd3"), Name = "Hounslow Central" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("03cafb34-fd9d-4069-ae3d-b61e83b53da6"), Name = "Hounslow East" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("75630ba5-3767-44da-b14a-5cb67caa1875"), Name = "Osterley" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("8f1b0a2d-93ff-41d2-bcce-7685972b83ba"), Name = "Boston Manor" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("8542a84b-b9a8-4937-a910-1e0b5f778377"), Name = "Northfields" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("8a1409d6-217c-4024-8494-c34689a89a6e"), Name = "South Ealing" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("82a3d734-abf8-43f1-853d-a69f938c20b1"), Name = "Acton Town" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("612f4d89-3086-4e1b-9771-d0e748718eb8"), Name = "Turnham Green" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("bdf7bc22-9e0b-4bfc-8abe-8130f5c462c8"), Name = "Hammersmith" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("6ea41ee3-5e14-4830-af0a-02d2bd9bcf98"), Name = "Barons Court" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("03c1cead-6d76-40f7-b67d-0eecef00220b"), Name = "Earl's Court" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("94b2e2cc-4150-4ab0-b387-21e2a13960e1"), Name = "Gloucester Road" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("4afcf7aa-f5ee-499f-9484-38e0b0c0af0b"), Name = "South Kensington" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("dc662ccf-3be3-40bb-ae03-4534bbaafe19"), Name = "Knightsbridge" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("a4965d09-d0fa-49f7-90bd-d550eb713670"), Name = "Hyde Park Corner" });
        resultStations.Stations[19].Should().BeEquivalentTo(new { Id = Guid.Parse("a88f9ee1-e742-44ea-96b1-467df4a561a2"), Name = "Green Park" });
        resultStations.Stations[20].Should().BeEquivalentTo(new { Id = Guid.Parse("da1c4ff7-2831-48f0-a7a2-112a03c68d37"), Name = "Piccadilly Circus" });
        resultStations.Stations[21].Should().BeEquivalentTo(new { Id = Guid.Parse("f8fe3b87-f765-44f0-9604-efa8ecd324f6"), Name = "Leicester Square" });
        resultStations.Stations[22].Should().BeEquivalentTo(new { Id = Guid.Parse("ad7af5d5-75be-4f42-9ea1-5191046ef6f8"), Name = "Covent Garden" });
        resultStations.Stations[23].Should().BeEquivalentTo(new { Id = Guid.Parse("92fef3de-c4db-47d2-ac5c-a188c6ab604c"), Name = "Holborn" });
        resultStations.Stations[24].Should().BeEquivalentTo(new { Id = Guid.Parse("361ec66d-3aed-4df6-aab6-b7fe955323b4"), Name = "Russell Square" });
        resultStations.Stations[25].Should().BeEquivalentTo(new { Id = Guid.Parse("5cf18e37-17e0-46c0-9177-6a5951df26b8"), Name = "King's Cross St.Pancras" });
        resultStations.Stations[26].Should().BeEquivalentTo(new { Id = Guid.Parse("36ce6d95-4979-4511-aef0-aa8f7b031838"), Name = "Caledonian Road" });
        resultStations.Stations[27].Should().BeEquivalentTo(new { Id = Guid.Parse("d39cb0f5-1858-41b7-b477-ade85012bb64"), Name = "Holloway Road" });
        resultStations.Stations[28].Should().BeEquivalentTo(new { Id = Guid.Parse("09ba8371-b0f3-431e-b549-8af06b991af0"), Name = "Arsenal" });
        resultStations.Stations[29].Should().BeEquivalentTo(new { Id = Guid.Parse("bc3918ec-947f-489e-aa05-3e2272232c78"), Name = "Finsbury Park" });
        resultStations.Stations[30].Should().BeEquivalentTo(new { Id = Guid.Parse("7dccb6db-2786-438a-b151-c5eaccdb1f9e"), Name = "Manor House" });
        resultStations.Stations[31].Should().BeEquivalentTo(new { Id = Guid.Parse("7ba4edc7-7177-4031-8f23-7f92bcc043fc"), Name = "Turnpike Lane" });
        resultStations.Stations[32].Should().BeEquivalentTo(new { Id = Guid.Parse("04fe0617-859e-457c-b8f3-42d050640d5e"), Name = "Wood Green" });
        resultStations.Stations[33].Should().BeEquivalentTo(new { Id = Guid.Parse("5ec78131-73b3-4b18-8e2f-d1b71eaa63d7"), Name = "Bounds Green" });
        resultStations.Stations[34].Should().BeEquivalentTo(new { Id = Guid.Parse("bb11562c-6862-43fa-b5b7-678a80d20032"), Name = "Arnos Grove" });
        resultStations.Stations[35].Should().BeEquivalentTo(new { Id = Guid.Parse("98ea0b91-b16b-4423-9e4b-1df0891019ea"), Name = "Southgate" });
        resultStations.Stations[36].Should().BeEquivalentTo(new { Id = Guid.Parse("68b4658a-683c-49b6-b9e1-ab5739f42d24"), Name = "Oakwood" });
        resultStations.Stations[37].Should().BeEquivalentTo(new { Id = Guid.Parse("392e1c83-1915-43ea-9685-aff7a8aa4bb5"), Name = "Cockfosters" });

        resultStations = result.ValidRoutes[2];
        resultStations.Stations.Count.Should().Be(42);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("28debada-e136-49a0-9143-9c0e5fb186e7"), Name = "Uxbridge" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("392e1c83-1915-43ea-9685-aff7a8aa4bb5"), Name = "Cockfosters" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("28debada-e136-49a0-9143-9c0e5fb186e7"), Name = "Uxbridge" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("7f51c4eb-68de-4438-9682-7ad860db94e2"), Name = "Hillingdon" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("b00e11cc-848c-41c9-91db-b5b19304030e"), Name = "Ickenham" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("e5ec6361-2137-44ff-bb43-5154ebbbf037"), Name = "Ruislip" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("0b8eff14-c36f-4506-98f9-d3c88fbb50ad"), Name = "Ruislip Manor" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("fa21a248-fd28-4ff3-aea7-1c4b4aced4a0"), Name = "Eastcote" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("50c69ee2-8dfb-4e26-9477-3c860e36ca3e"), Name = "Rayners Lane" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("f73e0bff-e0a4-42c8-a14a-2bae12a5e856"), Name = "South Harrow" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("59ac68a5-5c13-4100-97cd-f3c3b525429d"), Name = "Sudbury Hill" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("d61e03e4-417c-499e-8fe4-122b2a653941"), Name = "Sudbury Town" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("64349c04-603a-4fcf-99dd-63a7f4e68b1a"), Name = "Alperton" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("9965ebda-f2ec-450a-a04f-4be54d0f0106"), Name = "Park Royal" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("20375833-9e58-4b90-9e7c-65ebb63c5bbc"), Name = "North Ealing" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("e4c13d97-dc83-491e-8e2b-c80af1c21064"), Name = "Ealing Common" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("82a3d734-abf8-43f1-853d-a69f938c20b1"), Name = "Acton Town" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("612f4d89-3086-4e1b-9771-d0e748718eb8"), Name = "Turnham Green" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("bdf7bc22-9e0b-4bfc-8abe-8130f5c462c8"), Name = "Hammersmith" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("6ea41ee3-5e14-4830-af0a-02d2bd9bcf98"), Name = "Barons Court" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("03c1cead-6d76-40f7-b67d-0eecef00220b"), Name = "Earl's Court" });
        resultStations.Stations[19].Should().BeEquivalentTo(new { Id = Guid.Parse("94b2e2cc-4150-4ab0-b387-21e2a13960e1"), Name = "Gloucester Road" });
        resultStations.Stations[20].Should().BeEquivalentTo(new { Id = Guid.Parse("4afcf7aa-f5ee-499f-9484-38e0b0c0af0b"), Name = "South Kensington" });
        resultStations.Stations[21].Should().BeEquivalentTo(new { Id = Guid.Parse("dc662ccf-3be3-40bb-ae03-4534bbaafe19"), Name = "Knightsbridge" });
        resultStations.Stations[22].Should().BeEquivalentTo(new { Id = Guid.Parse("a4965d09-d0fa-49f7-90bd-d550eb713670"), Name = "Hyde Park Corner" });
        resultStations.Stations[23].Should().BeEquivalentTo(new { Id = Guid.Parse("a88f9ee1-e742-44ea-96b1-467df4a561a2"), Name = "Green Park" });
        resultStations.Stations[24].Should().BeEquivalentTo(new { Id = Guid.Parse("da1c4ff7-2831-48f0-a7a2-112a03c68d37"), Name = "Piccadilly Circus" });
        resultStations.Stations[25].Should().BeEquivalentTo(new { Id = Guid.Parse("f8fe3b87-f765-44f0-9604-efa8ecd324f6"), Name = "Leicester Square" });
        resultStations.Stations[26].Should().BeEquivalentTo(new { Id = Guid.Parse("ad7af5d5-75be-4f42-9ea1-5191046ef6f8"), Name = "Covent Garden" });
        resultStations.Stations[27].Should().BeEquivalentTo(new { Id = Guid.Parse("92fef3de-c4db-47d2-ac5c-a188c6ab604c"), Name = "Holborn" });
        resultStations.Stations[28].Should().BeEquivalentTo(new { Id = Guid.Parse("361ec66d-3aed-4df6-aab6-b7fe955323b4"), Name = "Russell Square" });
        resultStations.Stations[29].Should().BeEquivalentTo(new { Id = Guid.Parse("5cf18e37-17e0-46c0-9177-6a5951df26b8"), Name = "King's Cross St.Pancras" });
        resultStations.Stations[30].Should().BeEquivalentTo(new { Id = Guid.Parse("36ce6d95-4979-4511-aef0-aa8f7b031838"), Name = "Caledonian Road" });
        resultStations.Stations[31].Should().BeEquivalentTo(new { Id = Guid.Parse("d39cb0f5-1858-41b7-b477-ade85012bb64"), Name = "Holloway Road" });
        resultStations.Stations[32].Should().BeEquivalentTo(new { Id = Guid.Parse("09ba8371-b0f3-431e-b549-8af06b991af0"), Name = "Arsenal" });
        resultStations.Stations[33].Should().BeEquivalentTo(new { Id = Guid.Parse("bc3918ec-947f-489e-aa05-3e2272232c78"), Name = "Finsbury Park" });
        resultStations.Stations[34].Should().BeEquivalentTo(new { Id = Guid.Parse("7dccb6db-2786-438a-b151-c5eaccdb1f9e"), Name = "Manor House" });
        resultStations.Stations[35].Should().BeEquivalentTo(new { Id = Guid.Parse("7ba4edc7-7177-4031-8f23-7f92bcc043fc"), Name = "Turnpike Lane" });
        resultStations.Stations[36].Should().BeEquivalentTo(new { Id = Guid.Parse("04fe0617-859e-457c-b8f3-42d050640d5e"), Name = "Wood Green" });
        resultStations.Stations[37].Should().BeEquivalentTo(new { Id = Guid.Parse("5ec78131-73b3-4b18-8e2f-d1b71eaa63d7"), Name = "Bounds Green" });
        resultStations.Stations[38].Should().BeEquivalentTo(new { Id = Guid.Parse("bb11562c-6862-43fa-b5b7-678a80d20032"), Name = "Arnos Grove" });
        resultStations.Stations[39].Should().BeEquivalentTo(new { Id = Guid.Parse("98ea0b91-b16b-4423-9e4b-1df0891019ea"), Name = "Southgate" });
        resultStations.Stations[40].Should().BeEquivalentTo(new { Id = Guid.Parse("68b4658a-683c-49b6-b9e1-ab5739f42d24"), Name = "Oakwood" });
        resultStations.Stations[41].Should().BeEquivalentTo(new { Id = Guid.Parse("392e1c83-1915-43ea-9685-aff7a8aa4bb5"), Name = "Cockfosters" });
    }

    [Fact]
    public void RouteRepository_Suffragette_Routes_Correct()
    {
        var result = _repository.Lines[Guid.Parse("2cb7f0a1-fa5f-43bd-af91-18dd83623284")];

        result.Name.Should().Be("Suffragette");
        result.ValidRoutes.Count.Should().Be(1);

        var resultStations = result.ValidRoutes[0];
        resultStations.Stations.Count.Should().Be(13);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("79efd145-7794-40a0-a37a-f838223e641b"), Name = "Gospel Oak" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("a47b1c8c-559b-4798-b789-fc8e196c85a6"), Name = "Barking Riverside" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("79efd145-7794-40a0-a37a-f838223e641b"), Name = "Gospel Oak" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("de30a36b-be72-4684-9a67-68a09977562e"), Name = "Upper Holloway" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("3186bd9c-ca42-472e-bfcc-89a0c90137a8"), Name = "Crouch Hill" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("eade4a73-c86d-4c9b-9232-664d981aa511"), Name = "Harringay Green Lanes" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("3bedd194-aa6d-4dad-b4df-ab5d8f3f3543"), Name = "South Tottenham" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("9fff048a-a9e3-45a9-a553-32732a174f17"), Name = "Blackhorse Road" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("45df5e32-66ab-4a93-8a67-eeb07e81d284"), Name = "Walthamstow Queens Road" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("f40cfb96-e60f-411d-a0f0-cc18671a7287"), Name = "Leyton Midland Road" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("6cfd8295-cf69-48e3-9778-99d11d724c43"), Name = "Leytonstone High Road" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("467ea272-5f13-4cf3-afe7-b319d0b8997b"), Name = "Wanstead Park" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("ac28b830-4dcc-423c-87d5-d9b306e635c3"), Name = "Woodgrange Park" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("1c5faedb-30a6-4957-a8f7-6cdc702f4f68"), Name = "Barking" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("a47b1c8c-559b-4798-b789-fc8e196c85a6"), Name = "Barking Riverside" });
    }

    [Fact]
    public void RouteRepository_Tram_Routes_Correct()
    {
        var result = _repository.Lines[Guid.Parse("e466a866-6404-4a7d-a55d-a3e21d328e52")];

        result.Name.Should().Be("Tram");
        result.ValidRoutes.Count.Should().Be(3);

        var resultStations = result.ValidRoutes[0];
        resultStations.Stations.Count.Should().Be(27);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("1704b4d5-e5eb-4872-96d4-bd988a81802f"), Name = "Beckenham Junction" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("73a238b3-22e1-44f1-aa28-d0a7b41cea60"), Name = "Wimbledon" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("1704b4d5-e5eb-4872-96d4-bd988a81802f"), Name = "Beckenham Junction" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("cb653c05-e095-4a98-b600-f419370ba5eb"), Name = "Beckenham Road" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("0af1905b-f065-4157-ac19-8d40e1568ef7"), Name = "Avenue Road" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("c7c2d7d6-72fc-4bcc-9a5f-95f835f87196"), Name = "Birkbeck" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("8f4707e8-d43b-4dba-8b6e-c1b7b57d1838"), Name = "Harrington Road" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("817b2153-e822-40d4-b520-4a32ac934262"), Name = "Arena" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("ca948ebc-f14a-431b-af5b-918016c4f461"), Name = "Woodside" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("e80b2c38-0af3-4cca-8fb4-d00512b14a12"), Name = "Blackhorse Lane" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("d8f22c88-e04b-4cc2-b476-99949d1cdc6e"), Name = "Addiscombe" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("dda0b72c-23b2-433f-ac51-ee479f3c9093"), Name = "Sandilands" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("29debfff-8a70-4db3-9322-16fa7d099c8b"), Name = "Lebanon Road" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("5cf439f6-4390-465c-acc5-5d80a22f191e"), Name = "East Croydon" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("a26985d8-5f23-458c-97aa-4302d619fdb5"), Name = "George Street" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("1e7ac362-3cc6-414a-9dcf-47ae181eef6f"), Name = "Church Street" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("71594aa7-277a-4adb-bd70-b725f8e8f294"), Name = "Wandle Park" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("f48ec765-ba82-4eb4-b8d0-0c32f87cdb41"), Name = "Waddon Marsh" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("000dd8b7-65f4-4e03-ba7f-b74899fea3f0"), Name = "Ampere Way" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("387f45d9-59d3-431c-ae68-73d02a06cab5"), Name = "Therapia Lane" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("fdbccb5a-d1ba-4f52-a5da-f1984dc34c9c"), Name = "Beddington Lane" });
        resultStations.Stations[19].Should().BeEquivalentTo(new { Id = Guid.Parse("64475abc-54c5-46b1-a81b-4622c5d6bbea"), Name = "Mitcham Junction" });
        resultStations.Stations[20].Should().BeEquivalentTo(new { Id = Guid.Parse("7869026b-f79a-40eb-b71d-ce1fc301b5ab"), Name = "Mitcham" });
        resultStations.Stations[21].Should().BeEquivalentTo(new { Id = Guid.Parse("b6c40d99-cd52-4581-aa78-e7c167b20457"), Name = "Belgrave Walk" });
        resultStations.Stations[22].Should().BeEquivalentTo(new { Id = Guid.Parse("64933a13-0805-4247-a356-0cb98c28df72"), Name = "Phipps Bridge" });
        resultStations.Stations[23].Should().BeEquivalentTo(new { Id = Guid.Parse("13a346d3-1be0-4d5f-9b7e-e0097649da87"), Name = "Morden Road" });
        resultStations.Stations[24].Should().BeEquivalentTo(new { Id = Guid.Parse("4c251de7-a4ed-4f6b-a17d-c5fe0a7e50d7"), Name = "Merton Park" });
        resultStations.Stations[25].Should().BeEquivalentTo(new { Id = Guid.Parse("a4622324-dabb-4136-a0d3-b13ab28f2774"), Name = "Dundonald Road" });
        resultStations.Stations[26].Should().BeEquivalentTo(new { Id = Guid.Parse("73a238b3-22e1-44f1-aa28-d0a7b41cea60"), Name = "Wimbledon" });

        resultStations = result.ValidRoutes[1];
        resultStations.Stations.Count.Should().Be(23);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("35cedf0f-1a2e-4d76-b946-dbdfebd2a6af"), Name = "Elmers End" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("73a238b3-22e1-44f1-aa28-d0a7b41cea60"), Name = "Wimbledon" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("35cedf0f-1a2e-4d76-b946-dbdfebd2a6af"), Name = "Elmers End" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("817b2153-e822-40d4-b520-4a32ac934262"), Name = "Arena" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("ca948ebc-f14a-431b-af5b-918016c4f461"), Name = "Woodside" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("e80b2c38-0af3-4cca-8fb4-d00512b14a12"), Name = "Blackhorse Lane" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("d8f22c88-e04b-4cc2-b476-99949d1cdc6e"), Name = "Addiscombe" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("dda0b72c-23b2-433f-ac51-ee479f3c9093"), Name = "Sandilands" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("29debfff-8a70-4db3-9322-16fa7d099c8b"), Name = "Lebanon Road" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("5cf439f6-4390-465c-acc5-5d80a22f191e"), Name = "East Croydon" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("a26985d8-5f23-458c-97aa-4302d619fdb5"), Name = "George Street" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("1e7ac362-3cc6-414a-9dcf-47ae181eef6f"), Name = "Church Street" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("71594aa7-277a-4adb-bd70-b725f8e8f294"), Name = "Wandle Park" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("f48ec765-ba82-4eb4-b8d0-0c32f87cdb41"), Name = "Waddon Marsh" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("000dd8b7-65f4-4e03-ba7f-b74899fea3f0"), Name = "Ampere Way" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("387f45d9-59d3-431c-ae68-73d02a06cab5"), Name = "Therapia Lane" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("fdbccb5a-d1ba-4f52-a5da-f1984dc34c9c"), Name = "Beddington Lane" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("64475abc-54c5-46b1-a81b-4622c5d6bbea"), Name = "Mitcham Junction" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("7869026b-f79a-40eb-b71d-ce1fc301b5ab"), Name = "Mitcham" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("b6c40d99-cd52-4581-aa78-e7c167b20457"), Name = "Belgrave Walk" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("64933a13-0805-4247-a356-0cb98c28df72"), Name = "Phipps Bridge" });
        resultStations.Stations[19].Should().BeEquivalentTo(new { Id = Guid.Parse("13a346d3-1be0-4d5f-9b7e-e0097649da87"), Name = "Morden Road" });
        resultStations.Stations[20].Should().BeEquivalentTo(new { Id = Guid.Parse("4c251de7-a4ed-4f6b-a17d-c5fe0a7e50d7"), Name = "Merton Park" });
        resultStations.Stations[21].Should().BeEquivalentTo(new { Id = Guid.Parse("a4622324-dabb-4136-a0d3-b13ab28f2774"), Name = "Dundonald Road" });
        resultStations.Stations[22].Should().BeEquivalentTo(new { Id = Guid.Parse("73a238b3-22e1-44f1-aa28-d0a7b41cea60"), Name = "Wimbledon" });

        resultStations = result.ValidRoutes[2];
        resultStations.Stations.Count.Should().Be(12);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("73ad46e7-ca35-499c-8c87-526cd2fea255"), Name = "New Addington" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("1e7ac362-3cc6-414a-9dcf-47ae181eef6f"), Name = "Church Street" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("73ad46e7-ca35-499c-8c87-526cd2fea255"), Name = "New Addington" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("57a5cc2a-689d-44d9-94be-3ba62af8c1cd"), Name = "King Henry's Drive" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("5f65aa7f-5c1a-4e65-ac75-c5693740e282"), Name = "Fieldway" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("f6720c0d-a0d4-49a5-8e87-7efba76a5910"), Name = "Addington Village" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("7466ea37-99a5-4478-8e4d-95d0bb0759e5"), Name = "Gravel Hill" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("2a2e315e-eeb6-4ddd-9a1b-c687eef47856"), Name = "Coombe Lane" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("232f9ba6-84a2-4b58-8bca-1770a0d1fba5"), Name = "Lloyd Park" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("dda0b72c-23b2-433f-ac51-ee479f3c9093"), Name = "Sandilands" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("29debfff-8a70-4db3-9322-16fa7d099c8b"), Name = "Lebanon Road" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("5cf439f6-4390-465c-acc5-5d80a22f191e"), Name = "East Croydon" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("a26985d8-5f23-458c-97aa-4302d619fdb5"), Name = "George Street" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("1e7ac362-3cc6-414a-9dcf-47ae181eef6f"), Name = "Church Street" });
    }

    [Fact]
    public void RouteRepository_Victoria_Routes_Correct()
    {
        var result = _repository.Lines[Guid.Parse("9c834a1e-8a34-4c1e-943e-6f37b8e1e9d4")];

        result.Name.Should().Be("Victoria");
        result.ValidRoutes.Count.Should().Be(1);

        var resultStations = result.ValidRoutes[0];
        resultStations.Stations.Count.Should().Be(16);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("8f56cc51-2827-4fcb-8983-b1959c3c1e07"), Name = "Brixton" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("3373da0c-4111-47eb-b8aa-a2bcbcd1eca1"), Name = "Walthamstow Central" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("8f56cc51-2827-4fcb-8983-b1959c3c1e07"), Name = "Brixton" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("e7beb5c1-a574-421f-b92e-ea66acddc230"), Name = "Stockwell" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("68807451-ca6b-491d-9da6-722ce632ffa6"), Name = "Vauxhall" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("55dc6c6e-9fe8-4cf6-8880-ca4ec5c142a2"), Name = "Pimlico" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("9d0f8c47-3708-489a-99b3-1a6d960341e6"), Name = "Victoria" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("a88f9ee1-e742-44ea-96b1-467df4a561a2"), Name = "Green Park" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("246a422c-17d4-45ce-8cf9-0f36413d08e6"), Name = "Oxford Circus" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("e04078ef-639b-487e-b265-281db2f6b8a0"), Name = "Warren Street" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("cacbfade-1233-4922-9caa-aa8b0db9b3da"), Name = "Euston" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("5cf18e37-17e0-46c0-9177-6a5951df26b8"), Name = "King's Cross St.Pancras" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("dd66ccf1-1f07-492f-b42f-67fd107889c2"), Name = "Highbury & Islington" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("bc3918ec-947f-489e-aa05-3e2272232c78"), Name = "Finsbury Park" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("6611689e-ac78-4100-8b3a-60d6154c5cb2"), Name = "Seven Sisters" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("a4f65677-a82f-4d14-a5fd-b0128f28d6dc"), Name = "Tottenham Hale" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("9fff048a-a9e3-45a9-a553-32732a174f17"), Name = "Blackhorse Road" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("3373da0c-4111-47eb-b8aa-a2bcbcd1eca1"), Name = "Walthamstow Central" });
    }

    [Fact]
    public void RouteRepository_WaterlooAndCity_Routes_Correct()
    {
        var result = _repository.Lines[Guid.Parse("73c2b92d-ef29-4bbf-9f60-57a1f8ab7f50")];

        result.Name.Should().Be("Waterloo & City");
        result.ValidRoutes.Count.Should().Be(1);

        var resultStations = result.ValidRoutes[0];
        resultStations.Stations.Count.Should().Be(2);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("8cebdb43-8d17-49a2-a06b-1f3513091845"), Name = "Waterloo" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("aaedc653-e766-4d6b-87e2-4c87322971ef"), Name = "Bank" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("8cebdb43-8d17-49a2-a06b-1f3513091845"), Name = "Waterloo" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("aaedc653-e766-4d6b-87e2-4c87322971ef"), Name = "Bank" });
    }

    [Fact]
    public void RouteRepository_Weaver_Routes_Correct()
    {
        var result = _repository.Lines[Guid.Parse("0b3d9ef1-fdd6-4db8-8d37-34e9c6ab8385")];

        result.Name.Should().Be("Weaver");
        result.ValidRoutes.Count.Should().Be(3);

        var resultStations = result.ValidRoutes[0];
        resultStations.Stations.Count.Should().Be(11);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("db101bcd-350e-4485-b875-7ac2c8c1b6cc"), Name = "Liverpool Street" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("9bd86a33-3b5a-436e-9456-1660c4071e43"), Name = "Chingford" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("db101bcd-350e-4485-b875-7ac2c8c1b6cc"), Name = "Liverpool Street" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("b7a5ae67-882b-4509-8df9-4bae2ef1dd2a"), Name = "Bethnal Green" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("aa3d16ac-eff6-455d-be62-f2cb46bab923"), Name = "Cambridge Heath" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("29960244-35b1-4cda-b7bd-a99b7144445a"), Name = "London Fields" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("1457310e-fc09-47ff-9943-ac916388c781"), Name = "Hackney Downs" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("42c4aee1-e050-4127-951a-b5a018de2754"), Name = "Clapton" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("13a656c0-e690-46e0-9a57-5857c77dfb76"), Name = "St.James Street" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("3373da0c-4111-47eb-b8aa-a2bcbcd1eca1"), Name = "Walthamstow Central" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("5588ef92-4eb0-405e-9149-36620cdaca6f"), Name = "Wood Street" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("d388f6ac-9708-4bf5-a51e-ffe0bdc6036d"), Name = "Highams Park" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("9bd86a33-3b5a-436e-9456-1660c4071e43"), Name = "Chingford" });

        resultStations = result.ValidRoutes[1];
        resultStations.Stations.Count.Should().Be(15);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("db101bcd-350e-4485-b875-7ac2c8c1b6cc"), Name = "Liverpool Street" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("5147361f-a9b2-40ed-a52a-c300d71e1ffa"), Name = "Enfield Town" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("db101bcd-350e-4485-b875-7ac2c8c1b6cc"), Name = "Liverpool Street" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("b7a5ae67-882b-4509-8df9-4bae2ef1dd2a"), Name = "Bethnal Green" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("aa3d16ac-eff6-455d-be62-f2cb46bab923"), Name = "Cambridge Heath" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("29960244-35b1-4cda-b7bd-a99b7144445a"), Name = "London Fields" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("1457310e-fc09-47ff-9943-ac916388c781"), Name = "Hackney Downs" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("ca44b105-ec2a-4ba2-9834-c665da804a42"), Name = "Rectory Road" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("60bbfe9c-1f17-433d-80c6-e70d56db0936"), Name = "Stoke Newington" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("edf66991-e80d-42c5-9572-6618dbbf0e7d"), Name = "Stamford Hill" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("6611689e-ac78-4100-8b3a-60d6154c5cb2"), Name = "Seven Sisters" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("403a349c-41e8-456e-b588-1a1d715186ae"), Name = "Bruce Grove" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("71f891ab-0bec-4c8f-981b-9c67a05a6497"), Name = "White Hart Lane" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("0c13a71f-32f6-4e9c-a8a6-aecae68005bc"), Name = "Silver Street" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("843cf3f1-3eaa-4f4b-ad64-752489bbbfec"), Name = "Edmonton Green" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("239b3c41-1125-47a3-995c-3bd3e0c3c78b"), Name = "Bush Hill Park" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("5147361f-a9b2-40ed-a52a-c300d71e1ffa"), Name = "Enfield Town" });

        resultStations = result.ValidRoutes[2];
        resultStations.Stations.Count.Should().Be(17);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("db101bcd-350e-4485-b875-7ac2c8c1b6cc"), Name = "Liverpool Street" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("727d8469-e708-46c9-a722-5df5f237803f"), Name = "Cheshunt" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("db101bcd-350e-4485-b875-7ac2c8c1b6cc"), Name = "Liverpool Street" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("b7a5ae67-882b-4509-8df9-4bae2ef1dd2a"), Name = "Bethnal Green" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("aa3d16ac-eff6-455d-be62-f2cb46bab923"), Name = "Cambridge Heath" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("29960244-35b1-4cda-b7bd-a99b7144445a"), Name = "London Fields" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("1457310e-fc09-47ff-9943-ac916388c781"), Name = "Hackney Downs" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("ca44b105-ec2a-4ba2-9834-c665da804a42"), Name = "Rectory Road" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("60bbfe9c-1f17-433d-80c6-e70d56db0936"), Name = "Stoke Newington" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("edf66991-e80d-42c5-9572-6618dbbf0e7d"), Name = "Stamford Hill" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("6611689e-ac78-4100-8b3a-60d6154c5cb2"), Name = "Seven Sisters" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("403a349c-41e8-456e-b588-1a1d715186ae"), Name = "Bruce Grove" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("71f891ab-0bec-4c8f-981b-9c67a05a6497"), Name = "White Hart Lane" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("0c13a71f-32f6-4e9c-a8a6-aecae68005bc"), Name = "Silver Street" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("843cf3f1-3eaa-4f4b-ad64-752489bbbfec"), Name = "Edmonton Green" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("29776bb1-cda0-4241-b2b5-a476419abf6d"), Name = "Southbury" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("f1b095c2-d2aa-400f-9c72-373f19c9c522"), Name = "Turkey Street" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("99f76a26-d064-4044-8f10-741ef2a641f4"), Name = "Theobalds Grove" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("727d8469-e708-46c9-a722-5df5f237803f"), Name = "Cheshunt" });
    }

    [Fact]
    public void RouteRepository_Windrush_Routes_Correct()
    {
        var result = _repository.Lines[Guid.Parse("2f9d4b4c-c001-4be2-b02f-b227c1a7d84b")];

        result.Name.Should().Be("Windrush");
        result.ValidRoutes.Count.Should().Be(4);

        var resultStations = result.ValidRoutes[0];
        resultStations.Stations.Count.Should().Be(13);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("dd66ccf1-1f07-492f-b42f-67fd107889c2"), Name = "Highbury & Islington" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("8dfe7a6a-d27d-4442-ac33-fa92fbbfc7cd"), Name = "New Cross" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("dd66ccf1-1f07-492f-b42f-67fd107889c2"), Name = "Highbury & Islington" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("9c8db56b-4a81-4ae3-a5df-84ec7685ff82"), Name = "Canonbury" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("06611752-2c51-4cd2-ad49-6bffe078b729"), Name = "Dalston Junction" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("a3eb25a3-1dd8-4e4d-9eda-13a3efd307a0"), Name = "Haggerston" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("71a9939d-7bf7-4147-8e4e-6c862e14bad1"), Name = "Hoxton" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("af51f10d-feaf-40d7-924b-878557446278"), Name = "Shoreditch High Street" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("9787df93-f917-4890-9e0b-8b36e795bf9b"), Name = "Whitechapel" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("83c12f73-4e47-4aab-a10c-ff445a458a33"), Name = "Shadwell" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("3a382272-b641-41b2-987e-c1afcae4d31e"), Name = "Wapping" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("7946ceaf-2505-44d5-97bc-d140184abd55"), Name = "Rotherhithe" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("28cee11a-267d-4170-9cdc-2e7ef7b6ca40"), Name = "Canada Water" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("81da3785-9867-41d1-a990-9b2097dfbbab"), Name = "Surrey Quays" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("8dfe7a6a-d27d-4442-ac33-fa92fbbfc7cd"), Name = "New Cross" });

        resultStations = result.ValidRoutes[1];
        resultStations.Stations.Count.Should().Be(18);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("dd66ccf1-1f07-492f-b42f-67fd107889c2"), Name = "Highbury & Islington" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("d2282591-8e9f-440c-9220-a8f2428be388"), Name = "Crystal Palace" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("dd66ccf1-1f07-492f-b42f-67fd107889c2"), Name = "Highbury & Islington" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("9c8db56b-4a81-4ae3-a5df-84ec7685ff82"), Name = "Canonbury" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("06611752-2c51-4cd2-ad49-6bffe078b729"), Name = "Dalston Junction" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("a3eb25a3-1dd8-4e4d-9eda-13a3efd307a0"), Name = "Haggerston" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("71a9939d-7bf7-4147-8e4e-6c862e14bad1"), Name = "Hoxton" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("af51f10d-feaf-40d7-924b-878557446278"), Name = "Shoreditch High Street" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("9787df93-f917-4890-9e0b-8b36e795bf9b"), Name = "Whitechapel" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("83c12f73-4e47-4aab-a10c-ff445a458a33"), Name = "Shadwell" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("3a382272-b641-41b2-987e-c1afcae4d31e"), Name = "Wapping" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("7946ceaf-2505-44d5-97bc-d140184abd55"), Name = "Rotherhithe" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("28cee11a-267d-4170-9cdc-2e7ef7b6ca40"), Name = "Canada Water" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("81da3785-9867-41d1-a990-9b2097dfbbab"), Name = "Surrey Quays" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("854973df-ab4a-4a46-a987-e97d2305cf0c"), Name = "New Cross Gate" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("73780616-cad4-4c65-ad61-c92970dd9939"), Name = "Brockley" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("01a67d7b-1195-4aaf-ae5e-8a5ffc2b9914"), Name = "Honor Oak Park" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("091211f7-c875-4345-9ee4-0b6bb6836bb1"), Name = "Forest Hill" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("1b9dcfe8-bdf6-4e6a-800b-61e040c5d16b"), Name = "Sydenham" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("d2282591-8e9f-440c-9220-a8f2428be388"), Name = "Crystal Palace" });

        resultStations = result.ValidRoutes[2];
        resultStations.Stations.Count.Should().Be(21);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("dd66ccf1-1f07-492f-b42f-67fd107889c2"), Name = "Highbury & Islington" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("ea6c8400-b9b5-4c4e-ac22-3ace2ac15325"), Name = "West Croydon" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("dd66ccf1-1f07-492f-b42f-67fd107889c2"), Name = "Highbury & Islington" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("9c8db56b-4a81-4ae3-a5df-84ec7685ff82"), Name = "Canonbury" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("06611752-2c51-4cd2-ad49-6bffe078b729"), Name = "Dalston Junction" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("a3eb25a3-1dd8-4e4d-9eda-13a3efd307a0"), Name = "Haggerston" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("71a9939d-7bf7-4147-8e4e-6c862e14bad1"), Name = "Hoxton" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("af51f10d-feaf-40d7-924b-878557446278"), Name = "Shoreditch High Street" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("9787df93-f917-4890-9e0b-8b36e795bf9b"), Name = "Whitechapel" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("83c12f73-4e47-4aab-a10c-ff445a458a33"), Name = "Shadwell" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("3a382272-b641-41b2-987e-c1afcae4d31e"), Name = "Wapping" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("7946ceaf-2505-44d5-97bc-d140184abd55"), Name = "Rotherhithe" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("28cee11a-267d-4170-9cdc-2e7ef7b6ca40"), Name = "Canada Water" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("81da3785-9867-41d1-a990-9b2097dfbbab"), Name = "Surrey Quays" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("854973df-ab4a-4a46-a987-e97d2305cf0c"), Name = "New Cross Gate" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("73780616-cad4-4c65-ad61-c92970dd9939"), Name = "Brockley" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("01a67d7b-1195-4aaf-ae5e-8a5ffc2b9914"), Name = "Honor Oak Park" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("091211f7-c875-4345-9ee4-0b6bb6836bb1"), Name = "Forest Hill" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("1b9dcfe8-bdf6-4e6a-800b-61e040c5d16b"), Name = "Sydenham" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("0b2d21a9-a6d5-4237-bce8-96dc6cb73a4c"), Name = "Penge West" });
        resultStations.Stations[18].Should().BeEquivalentTo(new { Id = Guid.Parse("75e3bd04-04a6-4172-94e2-26c373310fc1"), Name = "Anerley" });
        resultStations.Stations[19].Should().BeEquivalentTo(new { Id = Guid.Parse("e9ff4388-945c-47d6-9549-20b660b2ecd8"), Name = "Norwood Junction" });
        resultStations.Stations[20].Should().BeEquivalentTo(new { Id = Guid.Parse("ea6c8400-b9b5-4c4e-ac22-3ace2ac15325"), Name = "West Croydon" });

        resultStations = result.ValidRoutes[3];
        resultStations.Stations.Count.Should().Be(18);

        resultStations.From.Should().BeEquivalentTo(new { Id = Guid.Parse("dd66ccf1-1f07-492f-b42f-67fd107889c2"), Name = "Highbury & Islington" });
        resultStations.To.Should().BeEquivalentTo(new { Id = Guid.Parse("bc1a08a6-11b7-49a4-955f-ba505cf2b555"), Name = "Clapham Junction" });

        resultStations.Stations[0].Should().BeEquivalentTo(new { Id = Guid.Parse("dd66ccf1-1f07-492f-b42f-67fd107889c2"), Name = "Highbury & Islington" });
        resultStations.Stations[1].Should().BeEquivalentTo(new { Id = Guid.Parse("9c8db56b-4a81-4ae3-a5df-84ec7685ff82"), Name = "Canonbury" });
        resultStations.Stations[2].Should().BeEquivalentTo(new { Id = Guid.Parse("06611752-2c51-4cd2-ad49-6bffe078b729"), Name = "Dalston Junction" });
        resultStations.Stations[3].Should().BeEquivalentTo(new { Id = Guid.Parse("a3eb25a3-1dd8-4e4d-9eda-13a3efd307a0"), Name = "Haggerston" });
        resultStations.Stations[4].Should().BeEquivalentTo(new { Id = Guid.Parse("71a9939d-7bf7-4147-8e4e-6c862e14bad1"), Name = "Hoxton" });
        resultStations.Stations[5].Should().BeEquivalentTo(new { Id = Guid.Parse("af51f10d-feaf-40d7-924b-878557446278"), Name = "Shoreditch High Street" });
        resultStations.Stations[6].Should().BeEquivalentTo(new { Id = Guid.Parse("9787df93-f917-4890-9e0b-8b36e795bf9b"), Name = "Whitechapel" });
        resultStations.Stations[7].Should().BeEquivalentTo(new { Id = Guid.Parse("83c12f73-4e47-4aab-a10c-ff445a458a33"), Name = "Shadwell" });
        resultStations.Stations[8].Should().BeEquivalentTo(new { Id = Guid.Parse("3a382272-b641-41b2-987e-c1afcae4d31e"), Name = "Wapping" });
        resultStations.Stations[9].Should().BeEquivalentTo(new { Id = Guid.Parse("7946ceaf-2505-44d5-97bc-d140184abd55"), Name = "Rotherhithe" });
        resultStations.Stations[10].Should().BeEquivalentTo(new { Id = Guid.Parse("28cee11a-267d-4170-9cdc-2e7ef7b6ca40"), Name = "Canada Water" });
        resultStations.Stations[11].Should().BeEquivalentTo(new { Id = Guid.Parse("81da3785-9867-41d1-a990-9b2097dfbbab"), Name = "Surrey Quays" });
        resultStations.Stations[12].Should().BeEquivalentTo(new { Id = Guid.Parse("a7431ad3-16f5-4542-add1-4c1147a45648"), Name = "Queens Road Peckham" });
        resultStations.Stations[13].Should().BeEquivalentTo(new { Id = Guid.Parse("418e3a21-7441-436a-a81a-a557b3a71921"), Name = "Peckham Rye" });
        resultStations.Stations[14].Should().BeEquivalentTo(new { Id = Guid.Parse("f1b90f6e-b4c9-4ba6-9420-1c012a1d4ad3"), Name = "Denmark Hill" });
        resultStations.Stations[15].Should().BeEquivalentTo(new { Id = Guid.Parse("b97b96c8-73d4-43fa-9844-ad4847184bdc"), Name = "Clapham High Street" });
        resultStations.Stations[16].Should().BeEquivalentTo(new { Id = Guid.Parse("cf272634-57a5-49ad-b8ab-81bacca04076"), Name = "Wandsworth Road" });
        resultStations.Stations[17].Should().BeEquivalentTo(new { Id = Guid.Parse("bc1a08a6-11b7-49a4-955f-ba505cf2b555"), Name = "Clapham Junction" });
    }

    [Fact]
    public void RouteRepository_GetStationsBetween_InCorrectLine_CorrectOrder_CorrectStations_GetsCorrectStations()
    {
        var start = new Model.Station(Guid.Parse("5c15a8f5-a21d-4567-97a4-3cbc095d2298"), "Canary Wharf");
        var end = new Model.Station(Guid.Parse("b02ebcb8-83a4-48e1-85d8-6e3fb21fa058"), "Stratford");

        var result = _repository.GetStationsBetween(
            Guid.Parse("5e8c1a94-5f0c-4d4d-8c4b-07bba9f5eb54"), start.Id, end.Id).ToList();

        result.Should().BeEmpty();
    }

    [Fact]
    public void RouteRepository_GetStationsBetween_CorrectLine_CorrectOrder_InCorrectStations_GetsCorrectStations()
    {
        var start = new Model.Station(Guid.Parse("6a65894f-41da-47e2-bfcc-68d877d2a9b2"), "West Brompton");
        var end = new Model.Station(Guid.Parse("b02ebcb8-83a4-48e1-85d8-6e3fb21fa058"),"Stratford");

        var result = _repository.GetStationsBetween(
            Guid.Parse("5e8c1a94-5f0c-4d4d-8c4b-07bba9f5eb54"), start.Id, end.Id).ToList();

        result.Should().BeEmpty();
    }

    [Fact]
    public void RouteRepository_GetStationsBetween_CorrectLine_CorrectOrder_CorrectStations_GetsCorrectStations()
    {
        var start = new Model.Station(Guid.Parse("5c15a8f5-a21d-4567-97a4-3cbc095d2298"), "Canary Wharf");
        var end = new Model.Station(Guid.Parse("b02ebcb8-83a4-48e1-85d8-6e3fb21fa058"), "Stratford");

        var result = _repository.GetStationsBetween(
            Guid.Parse("2f0c75a5-8149-49b7-9cc6-32e4a5246d7f"), start.Id, end.Id).ToList();

        result[0].Should().BeEquivalentTo(new { Id = Guid.Parse("5c15a8f5-a21d-4567-97a4-3cbc095d2298"), Name = "Canary Wharf" });
        result[1].Should().BeEquivalentTo(new { Id = Guid.Parse("6252902f-7fd2-45a8-a6d5-1f377e88b9be"), Name = "North Greenwich" });
        result[2].Should().BeEquivalentTo(new { Id = Guid.Parse("752cd9c1-bead-404f-b12a-aa93c212f2c2"), Name = "Canning Town" });
        result[3].Should().BeEquivalentTo(new { Id = Guid.Parse("968bc258-138c-45cf-83c0-599705285d25"), Name = "West Ham" });
        result[4].Should().BeEquivalentTo(new { Id = Guid.Parse("b02ebcb8-83a4-48e1-85d8-6e3fb21fa058"), Name = "Stratford" });
    }

    [Fact]
    public void RouteRepository_GetStationsBetween_CorrectLine_Opposite_Direction_CorrectStations_GetsCorrectStations()
    {
        var start = new Model.Station(Guid.Parse("b02ebcb8-83a4-48e1-85d8-6e3fb21fa058"), "Stratford");
        var end = new Model.Station(Guid.Parse("5c15a8f5-a21d-4567-97a4-3cbc095d2298"), "Canary Wharf");

        var result = _repository.GetStationsBetween(
            Guid.Parse("2f0c75a5-8149-49b7-9cc6-32e4a5246d7f"), start.Id, end.Id).ToList();

        result[0].Should().BeEquivalentTo(new { Id = Guid.Parse("b02ebcb8-83a4-48e1-85d8-6e3fb21fa058"), Name = "Stratford" });
        result[1].Should().BeEquivalentTo(new { Id = Guid.Parse("968bc258-138c-45cf-83c0-599705285d25"), Name = "West Ham" });
        result[2].Should().BeEquivalentTo(new { Id = Guid.Parse("752cd9c1-bead-404f-b12a-aa93c212f2c2"), Name = "Canning Town" });
        result[3].Should().BeEquivalentTo(new { Id = Guid.Parse("6252902f-7fd2-45a8-a6d5-1f377e88b9be"), Name = "North Greenwich" });
        result[4].Should().BeEquivalentTo(new { Id = Guid.Parse("5c15a8f5-a21d-4567-97a4-3cbc095d2298"), Name = "Canary Wharf" });
    }
}
