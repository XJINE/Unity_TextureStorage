using UnityEngine;

public class TexturePickerTimeShift : TexturePicker
{
    #region Field

    public int currentManagerIndex = 0;

    public float shiftTimeSec = 30;

    private float previousShiftTime = 0;

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
        if (Time.timeSinceLevelLoad - this.previousShiftTime > this.shiftTimeSec)
        {
            this.currentManagerIndex = (this.currentManagerIndex + 1)
                                     % base.textureManagers.Length;

            this.previousShiftTime = Time.timeSinceLevelLoad;
        }
    }

    public virtual TextureManager.TextureData Pick(bool withProportion = false)
    {
        return this.CurrentManager.RandomPick(withProportion);
    }

    #endregion Method
}