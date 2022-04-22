CREATE PROCEDURE [SetKnownGoodState]
AS
BEGIN
    TRUNCATE TABLE Agency;
    insert into Agency (AgencyId, ShortName, LongName) values (1, 'Aquamarine', 'Flatley LLC');
    insert into Agency (AgencyId, ShortName, LongName) values (2, 'Aquamarine', 'Veum Group');
    insert into Agency (AgencyId, ShortName, LongName) values (3, 'Fuscia', 'Jenkins-Metz');
    insert into Agency (AgencyId, ShortName, LongName) values (4, 'Pink', 'King Inc');
    insert into Agency (AgencyId, ShortName, LongName) values (5, 'Turquoise', 'Gleichner Inc');

END;