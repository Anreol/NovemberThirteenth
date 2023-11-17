using RoR2;
using UnityEngine;

namespace NovemberThirteenth
{
    internal class GummyBearMusicDisc : MonoBehaviour
    {
        public int rolledID;
        public uint playID;
        public bool hasPaused;
        public CharacterBody characterBody;
        public EntityStateMachine bodyStateMachine;
        public SetStateOnHurt hurtState;
        public void OnEnable()
        {
            if (playID == 0)
            {
                rolledID = UnityEngine.Random.Range(0, playEvents.Length);
                characterBody = gameObject.GetComponent<CharacterBody>();
                playID = Util.PlaySound(playEvents[rolledID], base.gameObject);
            }
            if (bodyStateMachine == null && characterBody)
            {
                bodyStateMachine = EntityStateMachine.FindByCustomName(characterBody.gameObject, "Body");
                hurtState = characterBody.GetComponent<SetStateOnHurt>();
            }
        }

        public void Update()
        {
            bool isStunned = false;
            bool isShocked = false;
            if (hurtState)
            {
                isStunned = bodyStateMachine.state.GetType() == hurtState.hurtState.stateType;
                isShocked = bodyStateMachine.state.GetType() == typeof(EntityStates.ShockState);
            }
            if ((characterBody.healthComponent.isInFrozenState || isShocked || isStunned ) && !hasPaused)
            {
                Util.PlaySound(pauseEvents[rolledID], base.gameObject);
                hasPaused = true;
            }
            if (!characterBody.healthComponent.isInFrozenState && !isShocked && !isStunned && hasPaused)
            {
                Util.PlaySound(resumeEvents[rolledID], base.gameObject);
                hasPaused = false;
            }
        }
        public void OnDisable()
        {
            AkSoundEngine.StopPlayingID(playID);
            playID = 0;
        }
        public static string[] playEvents = new string[]
        {
            "Play_vat19_gummy",
            "Play_november_13th"
        };
        public static string[] pauseEvents = new string[]
        {
            "Pause_vat19_gummy",
            "Pause_november_13th"
        };
        public static string[] resumeEvents = new string[]
        {
            "Resume_vat19_gummy",
            "Resume_november_13th"
        };
    }
}