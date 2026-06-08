# Jaffa Prim'ta implantation age eligibility

## Scope of 0.1.33-dev

This milestone adds the first biological-age gate for Jaffa Prim'ta
implantation.

## Lore-oriented threshold

The first gameplay threshold is:

```text
10 biological years
```

This represents the approximate Prata-age window when a young Jaffa becomes
eligible to receive an immature symbiote.

## Shared utility

The rule is centralized in:

```text
JaffaPrimtaUtility.MinimumPrimtaImplantationBiologicalAge
JaffaPrimtaUtility.MeetsPrimtaImplantationAge(...)
JaffaPrimtaUtility.IsEligibleForPrimtaImplantation(...)
```

## Medical operation behavior

```text
compatible Jaffa under 10 biological years
    ↓
operation hidden

compatible Jaffa aged 10+ without Prim'ta
    ↓
operation available

compatible Jaffa with existing Prim'ta
    ↓
operation hidden
```

The recipe worker also performs a defensive age check during execution and
shows a rejection message if an invalid bill reaches the surgery path.

## Scope boundary

This milestone does **not** add the puberty dependency yet.

A later milestone should add a progressive health penalty for Jaffa who reach
puberty without a Prim'ta or tretonin treatment.

## Test checklist

1. Build with `build.cmd`.
2. Spawn or generate one compatible Jaffa child below 10 biological years.
3. Confirm `implant Jaffa Prim'ta` is absent from the operations list.
4. Spawn or generate one compatible Jaffa aged exactly 10 or older.
5. Confirm the operation is present.
6. Supply one medicine and one physical larva.
7. Complete the surgery.
8. Confirm the Prim'ta Hediff appears.
9. Confirm a Jaffa with an existing Prim'ta cannot schedule a second operation.
10. Save and reload.
11. Confirm the Hediff remains persistent.
