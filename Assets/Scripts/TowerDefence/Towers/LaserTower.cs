﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util.Debugger;

namespace TowerDefence
{
    public class LaserTower : BaseTower
    {
        float fireDelay;
        float weaponCooldown;
        const float beamFadeTime = 0.3f;
        const float beamMaxWidth = 0.4f;
        const float beamMaxDamageScale = 30f; //NOT DAMAGE CAP! just for visuals.
        const float beamMinDamageScale = 1f;

        float beamOpacity;
        float beamWidth;
        float lastbeamWidth;
        LineRenderer line;

        protected override void Start()
        {
            base.Start();
            _type = "Laser";
            _fireRate = 120f;
            fireDelay = 60f / _fireRate;

            if(line == null)
            {
                GameObject g = new GameObject();

                g.transform.SetParent(transform);
                g.name = _type + " Beam renderer";
                line = g.AddComponent<LineRenderer>();
                line.useWorldSpace = true;

                setBeamPos(fireWorldPosition);

                line.startWidth = beamWidth;
                line.endWidth = beamWidth;

                line.material = Managers.LiveAssetManager.instance.laserMaterial;
            }
        }

        public override void IUpdate()
        {
            if (weaponCooldown <= 0)
            {
                base.IUpdate();
                if (Target)
                {
                    if (Target.VirtualHealth > 0)
                    {

                        setBeamPos(Target.transform.position);

                        lastbeamWidth = getLineWidth();
                        beamWidth = lastbeamWidth;
                        beamOpacity = 1f;

                        line.startWidth = beamWidth;
                        line.endWidth = beamWidth;

                        line.startColor = new Color(1, 1, 1, beamOpacity);
                        line.endColor = new Color(1, 1, 1, beamOpacity);

                        weaponCooldown = fireDelay;

                        Target.TakeVirtualDamage(_damage);
                        Target.TakeDamage(_damage);

                        Target = null;
                    }
                }
            }
            else
            {
                weaponCooldown -= Time.deltaTime;               
            }

            if (beamWidth > 0)
            {
                beamWidth -= (lastbeamWidth / beamFadeTime) * Time.deltaTime;
                beamOpacity -= (1f/beamFadeTime) * Time.deltaTime;
                if (beamWidth < 0)
                {
                    beamWidth = 0f;
                }
                line.startColor = new Color(1, 1, 1, beamOpacity);
                line.endColor = new Color(1, 1, 1, beamOpacity);
            }
            line.startWidth = beamWidth;
            line.endWidth = beamWidth;

            //beamWidth.QuickLog("BeamWidth" + name);
            //Time.time.QuickLog("TIME");
        }

        float getLineWidth()
        {
            float width;
            if (_damage <= beamMinDamageScale)
            {
                width = (beamMinDamageScale / beamMaxDamageScale) * beamMaxWidth;
            }
            else if (_damage >= beamMaxDamageScale)
            {
                width = beamMaxWidth;
            }
            else
            {
                width = (_damage / beamMaxDamageScale) * beamMaxWidth;
            }

            return width;
        }

        void setBeamPos(Vector3 p)
        {
            line.SetPosition(0, ((Vector3)fireWorldPosition) + new Vector3(0, 0, -3));
            line.SetPosition(1, p + new Vector3(0, 0, -3));
        }
    }
}
