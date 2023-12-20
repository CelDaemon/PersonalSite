namespace PersonalSite.Model;

[Flags]
public enum DiscordFlag
{
    Staff = 1 << 0,
    Partner = 1 << 1,
    HypeSquad = 1 << 2,
    BugHunterLevel1 = 1 << 3,
    HouseBravery = 1 << 6,
    HouseBrilliance = 1 << 7,
    HouseBalance = 1 << 8,
    PremiumEarlySupporter = 1 << 9,
    TeamPseudoUser = 1 << 10,
    BugHunterLevel2 = 1 << 14,
    VerifiedBot = 1 << 16,
    VerifiedDeveloper = 1 << 17,
    CertifiedModerator = 1 << 18,
    BotHttpInteractions = 1 << 19,
    ActiveDeveloper = 1 << 22
}