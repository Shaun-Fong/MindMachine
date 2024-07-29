# MindMachine[WIP🔨]

<p align="center">
A behavior machine-based AI framework for Unity3D.
</p>

<p align="center">
Make game AI development easy 🎉.
</p>

<p align="center">
    <img src="https://github.com/user-attachments/assets/329af344-8048-4eed-886e-3f0ef237ffa1" alt="Follow" style="width: 200px;"/>
    <img src="https://github.com/user-attachments/assets/97a91848-7604-4a30-8738-2b0e3f76ccf6" alt="Avoid" style="width: 200px;"/>
    <img src="https://github.com/user-attachments/assets/b37b649b-ef3d-4677-9432-f3a287cd6f42" alt="Attack" style="width: 200px;"/>
</p>

## Example
``` C#
//Attack behavior
public override async UniTask Tick(Attacker instance)
{
    //Wait 1 second.
    await UniTask.Delay(1000);

    //Show marker.
    await instance.ShowMark(CancelToken);

    //Move to player
    await instance.MoveTo(PlayerController.Instance.transform.position, CancelToken).AttachExternalCancellation(CancelToken);

    //Attack!
    await instance.Attack(PlayerController.Instance, CancelToken);
}
```

## Assets
[Dino Characters by Arks](https://twitter.com/ScissorMarks)
 
[NinjaAdventure by pixel-boy](https://github.com/pixel-boy/NinjaAdventure)
