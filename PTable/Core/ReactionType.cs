namespace ChemSharp.PTable.Core;

/// <summary>
/// Represents a type of reaction.
/// </summary>
public enum ReactionType
{
    Synthesis,
    Decomposition,
    SingleReplacement,
    Precipitate,
    AcidBase,
    Combustion,
    NoReaction // used exclusively for when a single replacement reaction would not occur because the lone cation/anion is less reactive than that of the compound
}