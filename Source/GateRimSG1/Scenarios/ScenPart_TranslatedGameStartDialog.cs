using RimWorld;
using UnityEngine;
using Verse;

namespace GateRimSG1.Scenarios
{
    /// <summary>
    /// Displays a translated game-start narrative while keeping the resolved
    /// text visible in the scenario editor.
    ///
    /// Vanilla ScenPart_GameStartDialog edits only its private `text` field.
    /// A scenario that provides only `textKey` therefore appears blank in the
    /// editor even though the key exists. This dedicated part resolves the key
    /// explicitly and uses it as a robust fallback when the map starts.
    /// </summary>
    public class ScenPart_TranslatedGameStartDialog : ScenPart
    {
        public string text = string.Empty;
        public string textKey;
        public SoundDef closeSound;

        public override void DoEditInterface(Listing_ScenEdit listing)
        {
            Rect rect = listing.GetScenPartRect(
                this,
                RowHeight * 5f);

            if (text.NullOrEmpty())
            {
                text = ResolveText();
            }

            text = Widgets.TextArea(
                rect,
                text);
        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(ref text, "text");
            Scribe_Values.Look(ref textKey, "textKey");
            Scribe_Defs.Look(ref closeSound, "closeSound");
        }

        public override void PostGameStart()
        {
            if (!Find.GameInitData.startedFromEntry)
            {
                return;
            }

            string resolvedText = ResolveText();

            Find.MusicManagerPlay.disabled = true;
            Find.WindowStack.Notify_GameStartDialogOpened();

            DiaNode node = new DiaNode(resolvedText);
            DiaOption option = new DiaOption
            {
                resolveTree = true,
                clickSound = null
            };

            node.options.Add(option);

            Dialog_NodeTree dialog = new Dialog_NodeTree(node)
            {
                soundClose = closeSound ?? SoundDefOf.GameStartSting,
                closeAction = delegate
                {
                    Find.MusicManagerPlay.ForceSilenceFor(7f);
                    Find.MusicManagerPlay.disabled = false;
                    Find.WindowStack.Notify_GameStartDialogClosed();
                    Find.TickManager.CurTimeSpeed = TimeSpeed.Normal;
                    TutorSystem.Notify_Event("GameStartDialogClosed");
                }
            };

            Find.WindowStack.Add(dialog);
            Find.Archive.Add(new ArchivedDialog(node.text));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode()
                ^ text.GetHashCodeSafe()
                ^ textKey.GetHashCodeSafe()
                ^ (closeSound?.GetHashCode() ?? 0);
        }

        private string ResolveText()
        {
            if (!text.NullOrEmpty())
            {
                return text;
            }

            if (!textKey.NullOrEmpty())
            {
                return textKey.TranslateSimple();
            }

            return string.Empty;
        }
    }
}
