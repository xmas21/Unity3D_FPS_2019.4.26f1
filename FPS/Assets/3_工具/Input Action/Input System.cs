// GENERATED AUTOMATICALLY FROM 'Assets/3_工具/Input Action/Input System.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputSystem : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputSystem()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input System"",
    ""maps"": [
        {
            ""name"": ""Character"",
            ""id"": ""f6e40627-fec8-4433-8d69-fb0056658b6e"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""0bc2b03b-c227-4420-855b-53b34b046a2f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""View"",
                    ""type"": ""PassThrough"",
                    ""id"": ""9d5a5862-eed8-4753-97f9-aa78721a8f11"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""dd2bfd73-6aab-4402-bfa3-3209a48d2f0a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""6f6ee2e8-54e2-4afd-8c50-f54b6cbbb18a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CrouchRelease"",
                    ""type"": ""Button"",
                    ""id"": ""79d8f296-bdf2-47d1-a159-9b7b8d89d00c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Prone"",
                    ""type"": ""Button"",
                    ""id"": ""442c95ff-d583-47dd-96c7-a7591974b39c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""a413d526-513d-4199-9be0-559d222fe4e7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SprintRelease"",
                    ""type"": ""Button"",
                    ""id"": ""c5e6be11-698f-4a63-b1b0-e78c267410f5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Setting"",
                    ""type"": ""Button"",
                    ""id"": ""b6f0c195-cd59-4323-8ebc-7457b825942a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1963e5f4-2077-49f1-bbf9-28c684a32bcc"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""View"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""b465d6c1-55d5-441a-b8b0-ffa95fb68c13"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7ac5e737-b1de-489e-95ca-c2b77526c56d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""cf3232e4-8fa8-457c-8b5d-3d7c52a9872c"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""200d3c66-0c7b-4ba0-afa5-d8a72368af40"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3f3a4fcb-29c3-4cfb-971c-598b33c88dc7"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7f50f8fa-6255-4e0e-9098-87c41c5160af"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""60546680-550e-4612-93ab-0cce4b6ad766"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""18f04a9f-10e8-4f08-b374-ee005dd06fa9"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Prone"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2693ebbb-a337-4ab5-a188-21e7aebdb5ad"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1449d82d-4a34-4780-b758-a94fa55d3fb9"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SprintRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""25c583c4-bbbc-40d7-83b4-1e8f82da396d"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CrouchRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""55adadeb-3fc7-4118-bf84-92ba35cf7856"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Setting"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Weapon"",
            ""id"": ""9ee2770c-346d-4598-a24d-0438397a6ccd"",
            ""actions"": [
                {
                    ""name"": ""Fire2Press"",
                    ""type"": ""Button"",
                    ""id"": ""c3854a0d-4fcc-4dd0-8116-c0650c7478a7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire2Release"",
                    ""type"": ""Button"",
                    ""id"": ""25b5be81-c95b-4e80-95be-7afc80f8a93e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8f0d86c3-9e5c-4a9a-8fe6-491ca4216a86"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire2Press"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bb34145d-8c15-4eff-b71b-fa8b935b44c4"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire2Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Character
        m_Character = asset.FindActionMap("Character", throwIfNotFound: true);
        m_Character_Movement = m_Character.FindAction("Movement", throwIfNotFound: true);
        m_Character_View = m_Character.FindAction("View", throwIfNotFound: true);
        m_Character_Jump = m_Character.FindAction("Jump", throwIfNotFound: true);
        m_Character_Crouch = m_Character.FindAction("Crouch", throwIfNotFound: true);
        m_Character_CrouchRelease = m_Character.FindAction("CrouchRelease", throwIfNotFound: true);
        m_Character_Prone = m_Character.FindAction("Prone", throwIfNotFound: true);
        m_Character_Sprint = m_Character.FindAction("Sprint", throwIfNotFound: true);
        m_Character_SprintRelease = m_Character.FindAction("SprintRelease", throwIfNotFound: true);
        m_Character_Setting = m_Character.FindAction("Setting", throwIfNotFound: true);
        // Weapon
        m_Weapon = asset.FindActionMap("Weapon", throwIfNotFound: true);
        m_Weapon_Fire2Press = m_Weapon.FindAction("Fire2Press", throwIfNotFound: true);
        m_Weapon_Fire2Release = m_Weapon.FindAction("Fire2Release", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Character
    private readonly InputActionMap m_Character;
    private ICharacterActions m_CharacterActionsCallbackInterface;
    private readonly InputAction m_Character_Movement;
    private readonly InputAction m_Character_View;
    private readonly InputAction m_Character_Jump;
    private readonly InputAction m_Character_Crouch;
    private readonly InputAction m_Character_CrouchRelease;
    private readonly InputAction m_Character_Prone;
    private readonly InputAction m_Character_Sprint;
    private readonly InputAction m_Character_SprintRelease;
    private readonly InputAction m_Character_Setting;
    public struct CharacterActions
    {
        private @InputSystem m_Wrapper;
        public CharacterActions(@InputSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Character_Movement;
        public InputAction @View => m_Wrapper.m_Character_View;
        public InputAction @Jump => m_Wrapper.m_Character_Jump;
        public InputAction @Crouch => m_Wrapper.m_Character_Crouch;
        public InputAction @CrouchRelease => m_Wrapper.m_Character_CrouchRelease;
        public InputAction @Prone => m_Wrapper.m_Character_Prone;
        public InputAction @Sprint => m_Wrapper.m_Character_Sprint;
        public InputAction @SprintRelease => m_Wrapper.m_Character_SprintRelease;
        public InputAction @Setting => m_Wrapper.m_Character_Setting;
        public InputActionMap Get() { return m_Wrapper.m_Character; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterActions set) { return set.Get(); }
        public void SetCallbacks(ICharacterActions instance)
        {
            if (m_Wrapper.m_CharacterActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnMovement;
                @View.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnView;
                @View.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnView;
                @View.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnView;
                @Jump.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnJump;
                @Crouch.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnCrouch;
                @CrouchRelease.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnCrouchRelease;
                @CrouchRelease.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnCrouchRelease;
                @CrouchRelease.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnCrouchRelease;
                @Prone.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnProne;
                @Prone.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnProne;
                @Prone.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnProne;
                @Sprint.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnSprint;
                @SprintRelease.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnSprintRelease;
                @SprintRelease.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnSprintRelease;
                @SprintRelease.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnSprintRelease;
                @Setting.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnSetting;
                @Setting.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnSetting;
                @Setting.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnSetting;
            }
            m_Wrapper.m_CharacterActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @View.started += instance.OnView;
                @View.performed += instance.OnView;
                @View.canceled += instance.OnView;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @CrouchRelease.started += instance.OnCrouchRelease;
                @CrouchRelease.performed += instance.OnCrouchRelease;
                @CrouchRelease.canceled += instance.OnCrouchRelease;
                @Prone.started += instance.OnProne;
                @Prone.performed += instance.OnProne;
                @Prone.canceled += instance.OnProne;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @SprintRelease.started += instance.OnSprintRelease;
                @SprintRelease.performed += instance.OnSprintRelease;
                @SprintRelease.canceled += instance.OnSprintRelease;
                @Setting.started += instance.OnSetting;
                @Setting.performed += instance.OnSetting;
                @Setting.canceled += instance.OnSetting;
            }
        }
    }
    public CharacterActions @Character => new CharacterActions(this);

    // Weapon
    private readonly InputActionMap m_Weapon;
    private IWeaponActions m_WeaponActionsCallbackInterface;
    private readonly InputAction m_Weapon_Fire2Press;
    private readonly InputAction m_Weapon_Fire2Release;
    public struct WeaponActions
    {
        private @InputSystem m_Wrapper;
        public WeaponActions(@InputSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @Fire2Press => m_Wrapper.m_Weapon_Fire2Press;
        public InputAction @Fire2Release => m_Wrapper.m_Weapon_Fire2Release;
        public InputActionMap Get() { return m_Wrapper.m_Weapon; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(WeaponActions set) { return set.Get(); }
        public void SetCallbacks(IWeaponActions instance)
        {
            if (m_Wrapper.m_WeaponActionsCallbackInterface != null)
            {
                @Fire2Press.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnFire2Press;
                @Fire2Press.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnFire2Press;
                @Fire2Press.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnFire2Press;
                @Fire2Release.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnFire2Release;
                @Fire2Release.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnFire2Release;
                @Fire2Release.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnFire2Release;
            }
            m_Wrapper.m_WeaponActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Fire2Press.started += instance.OnFire2Press;
                @Fire2Press.performed += instance.OnFire2Press;
                @Fire2Press.canceled += instance.OnFire2Press;
                @Fire2Release.started += instance.OnFire2Release;
                @Fire2Release.performed += instance.OnFire2Release;
                @Fire2Release.canceled += instance.OnFire2Release;
            }
        }
    }
    public WeaponActions @Weapon => new WeaponActions(this);
    public interface ICharacterActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnView(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnCrouchRelease(InputAction.CallbackContext context);
        void OnProne(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnSprintRelease(InputAction.CallbackContext context);
        void OnSetting(InputAction.CallbackContext context);
    }
    public interface IWeaponActions
    {
        void OnFire2Press(InputAction.CallbackContext context);
        void OnFire2Release(InputAction.CallbackContext context);
    }
}
