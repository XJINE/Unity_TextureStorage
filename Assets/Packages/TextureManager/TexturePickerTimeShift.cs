using UnityEngine;

public class TexturePickerTimeShift : TexturePicker
{
    #region Field

    public int currentManagerIndex = 0;

    public float shiftTimeSec = 30;

    private float previousShiftTimeSec = 0;

    #endregion Field

    #region Property

    public TextureManager CurrentManager
    {
        get { return base.textureManagers[this.currentManagerIndex]; }
    }

    #endregion Property

    #region Method

    protected virtual void Update()
    {
        if (Time.timeSinceLevelLoad - this.previousShiftTimeSec > this.shiftTimeSec)
        {
            this.currentManagerIndex  = (this.currentManagerIndex + 1) % base.textureManagers.Length;
            this.previousShiftTimeSec = Time.timeSinceLevelLoad;
        }
    }

    public override TextureData RandomPick()
    {
        return this.CurrentManager.RandomPick();
    }

    #endregion Method
}