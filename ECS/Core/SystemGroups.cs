﻿namespace ME.ECS {

    using ME.ECS.Collections;
    using System.Collections.Generic;

    public enum RuntimeSystemFlag {

        None = 0x0,
        Logic = 0x1,
        Visual = 0x2,
        All = RuntimeSystemFlag.Logic | RuntimeSystemFlag.Visual,

    }
    
    public struct RuntimeSystem {

        internal ListCopyable<ISystemBase> allSystems;
        internal ListCopyable<IUpdate> systemUpdates;
        internal ListCopyable<ISystemFilter> systemFilters;
        internal ListCopyable<ILoadableSystem> systemLoadable;
        internal ListCopyable<IAdvanceTick> systemAdvanceTick;
        internal ListCopyable<IAdvanceTickPre> systemAdvanceTickPre;
        internal ListCopyable<IAdvanceTickPost> systemAdvanceTickPost;

        public T Get<T>() where T : ISystemBase {

            if (this.allSystems != null) {

                for (int i = 0; i < this.allSystems.Count; ++i) {

                    if (this.allSystems[i] is T sys) return sys;

                }
                
            }
            
            return default;
            
        }
        
        public bool IsSystemActive(ISystemBase system, RuntimeSystemFlag state) {

            {
                var arr = this.systemUpdates;
                if ((state & RuntimeSystemFlag.Visual) != 0 && arr != null) {
                    for (int i = 0; i < arr.Count; ++i) if (arr[i] == system) return true;
                }
            }

            {
                var arr = this.systemFilters;
                if ((state & RuntimeSystemFlag.Logic) != 0 && arr != null) {
                    for (int i = 0; i < arr.Count; ++i) if (arr[i] == system) return true;
                }
            }

            {
                var arr = this.systemLoadable;
                if ((state & RuntimeSystemFlag.Logic) != 0 && arr != null) {
                    for (int i = 0; i < arr.Count; ++i) if (arr[i] == system) return true;
                }
            }

            {
                var arr = this.systemAdvanceTick;
                if ((state & RuntimeSystemFlag.Logic) != 0 && arr != null) {
                    for (int i = 0; i < arr.Count; ++i) if (arr[i] == system) return true;
                }
            }

            {
                var arr = this.systemAdvanceTickPre;
                if ((state & RuntimeSystemFlag.Logic) != 0 && arr != null) {
                    for (int i = 0; i < arr.Count; ++i) if (arr[i] == system) return true;
                }
            }

            {
                var arr = this.systemAdvanceTickPost;
                if ((state & RuntimeSystemFlag.Logic) != 0 && arr != null) {
                    for (int i = 0; i < arr.Count; ++i) if (arr[i] == system) return true;
                }
            }
            
            return false;

        }
        
        public bool Has<T>(RuntimeSystemFlag state = RuntimeSystemFlag.All) where T : class, ISystemBase, new() {

            {
                var arr = this.systemUpdates;
                if ((state & RuntimeSystemFlag.Visual) != 0 && arr != null) {
                    for (int i = 0; i < arr.Count; ++i) if (arr[i] is T) return true;
                }
            }

            {
                var arr = this.systemFilters;
                if ((state & RuntimeSystemFlag.Logic) != 0 && arr != null) {
                    for (int i = 0; i < arr.Count; ++i) if (arr[i] is T) return true;
                }
            }

            {
                var arr = this.systemLoadable;
                if ((state & RuntimeSystemFlag.Logic) != 0 && arr != null) {
                    for (int i = 0; i < arr.Count; ++i) if (arr[i] is T) return true;
                }
            }

            {
                var arr = this.systemAdvanceTick;
                if ((state & RuntimeSystemFlag.Logic) != 0 && arr != null) {
                    for (int i = 0; i < arr.Count; ++i) if (arr[i] is T) return true;
                }
            }

            {
                var arr = this.systemAdvanceTickPre;
                if ((state & RuntimeSystemFlag.Logic) != 0 && arr != null) {
                    for (int i = 0; i < arr.Count; ++i) if (arr[i] is T) return true;
                }
            }

            {
                var arr = this.systemAdvanceTickPost;
                if ((state & RuntimeSystemFlag.Logic) != 0 && arr != null) {
                    for (int i = 0; i < arr.Count; ++i) if (arr[i] is T) return true;
                }
            }

            return false;
            
        }

        public void Add(ISystemBase system, RuntimeSystemFlag state = RuntimeSystemFlag.All) {

            {
                if (this.allSystems == null) this.allSystems = PoolList<ISystemBase>.Spawn(4);
                this.allSystems.Add(system);
            }
            {
                if ((state & RuntimeSystemFlag.Visual) != 0 && system is IUpdate systemTyped) {
                    if (this.systemUpdates == null) this.systemUpdates = PoolList<IUpdate>.Spawn(4);
                    this.systemUpdates.Add(systemTyped);
                }
            }
            {
                if ((state & RuntimeSystemFlag.Logic) != 0 && system is ISystemFilter systemTyped) {
                    if (this.systemFilters == null) this.systemFilters = PoolList<ISystemFilter>.Spawn(4);
                    this.systemFilters.Add(systemTyped);
                }
            }
            {
                if ((state & RuntimeSystemFlag.Logic) != 0 && system is ILoadableSystem systemTyped) {
                    if (this.systemLoadable == null) this.systemLoadable = PoolList<ILoadableSystem>.Spawn(4);
                    this.systemLoadable.Add(systemTyped);
                }
            }
            {
                if ((state & RuntimeSystemFlag.Logic) != 0 && system is IAdvanceTick systemTyped) {
                    if (this.systemAdvanceTick == null) this.systemAdvanceTick = PoolList<IAdvanceTick>.Spawn(4);
                    this.systemAdvanceTick.Add(systemTyped);
                }
            }
            {
                if ((state & RuntimeSystemFlag.Logic) != 0 && system is IAdvanceTickPre systemTyped) {
                    if (this.systemAdvanceTickPre == null) this.systemAdvanceTickPre = PoolList<IAdvanceTickPre>.Spawn(4);
                    this.systemAdvanceTickPre.Add(systemTyped);
                }
            }
            {
                if ((state & RuntimeSystemFlag.Logic) != 0 && system is IAdvanceTickPost systemTyped) {
                    if (this.systemAdvanceTickPost == null) this.systemAdvanceTickPost = PoolList<IAdvanceTickPost>.Spawn(4);
                    this.systemAdvanceTickPost.Add(systemTyped);
                }
            }

        }

        public bool Remove(ISystemBase system, RuntimeSystemFlag state = RuntimeSystemFlag.All) {
            
            var hasAny = false;
            {
                if (this.allSystems == null) this.allSystems = PoolList<ISystemBase>.Spawn(4);
                hasAny = this.allSystems.Remove(system);
            }
            {
                if ((state & RuntimeSystemFlag.Logic) != 0 && system is ISystemFilter systemTyped) {
                    if (this.systemFilters == null) this.systemFilters = PoolList<ISystemFilter>.Spawn(4);
                    this.systemFilters.Remove(systemTyped);
                }
            }
            {
                if ((state & RuntimeSystemFlag.Visual) != 0 && system is IUpdate systemTyped) {
                    if (this.systemUpdates == null) this.systemUpdates = PoolList<IUpdate>.Spawn(4);
                    this.systemUpdates.Remove(systemTyped);
                }
            }
            {
                if ((state & RuntimeSystemFlag.Logic) != 0 && system is ILoadableSystem systemTyped) {
                    if (this.systemLoadable == null) this.systemLoadable = PoolList<ILoadableSystem>.Spawn(4);
                    this.systemLoadable.Remove(systemTyped);
                }
            }
            {
                if ((state & RuntimeSystemFlag.Logic) != 0 && system is IAdvanceTick systemTyped) {
                    if (this.systemAdvanceTick == null) this.systemAdvanceTick = PoolList<IAdvanceTick>.Spawn(4);
                    this.systemAdvanceTick.Remove(systemTyped);
                }
            }
            {
                if ((state & RuntimeSystemFlag.Logic) != 0 && system is IAdvanceTickPre systemTyped) {
                    if (this.systemAdvanceTickPre == null) this.systemAdvanceTickPre = PoolList<IAdvanceTickPre>.Spawn(4);
                    this.systemAdvanceTickPre.Remove(systemTyped);
                }
            }
            {
                if ((state & RuntimeSystemFlag.Logic) != 0 && system is IAdvanceTickPost systemTyped) {
                    if (this.systemAdvanceTickPost == null) this.systemAdvanceTickPost = PoolList<IAdvanceTickPost>.Spawn(4);
                    this.systemAdvanceTickPost.Remove(systemTyped);
                }
            }

            return hasAny;

        }

        public void Deconstruct() {

            {
                var arr = this.systemFilters;
                if (arr != null) {
                    for (int i = 0; i < arr.Count; ++i) {
                        arr[i].filter = Filter.Empty;
                    }
                }
            }
            {
                var arr = this.allSystems;
                if (arr != null) {
                    for (int i = 0; i < arr.Count; ++i) {
                        arr[i].OnDeconstruct();
                        PoolSystems.Recycle(arr[i]);
                    }
                }
            }
            
            if (this.allSystems != null) PoolList<ISystemBase>.Recycle(ref this.allSystems);
            if (this.systemUpdates != null) PoolList<IUpdate>.Recycle(ref this.systemUpdates);
            if (this.systemFilters != null) PoolList<ISystemFilter>.Recycle(ref this.systemFilters);
            if (this.systemLoadable != null) PoolList<ILoadableSystem>.Recycle(ref this.systemLoadable);
            if (this.systemAdvanceTick != null) PoolList<IAdvanceTick>.Recycle(ref this.systemAdvanceTick);
            if (this.systemAdvanceTickPre != null) PoolList<IAdvanceTickPre>.Recycle(ref this.systemAdvanceTickPre);
            if (this.systemAdvanceTickPost != null) PoolList<IAdvanceTickPost>.Recycle(ref this.systemAdvanceTickPost);

        }

    }
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false)]
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false)]
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public struct SystemGroup {

        public string name;
        internal World world;
        internal RuntimeSystem runtimeSystem;
        internal int worldIndex;

        public SystemGroup(string name) : this(Worlds.currentWorld, name) {}

        public SystemGroup(World world, string name) {

            this.name = name;
            this.world = world;
            this.worldIndex = -1;
            this.runtimeSystem = new RuntimeSystem();
            this.worldIndex = world.AddSystemGroup(ref this);

        }
        
        internal void Deconstruct() {

            this.runtimeSystem.Deconstruct();

        }

        public bool IsSystemActive(ISystemBase system, RuntimeSystemFlag state) {

            return this.runtimeSystem.IsSystemActive(system, state);
            
        }
        
        /// <summary>
        /// Returns true if system with TSystem type exists
        /// </summary>
        /// <typeparam name="TSystem"></typeparam>
        /// <returns></returns>
        public bool HasSystem<TSystem>() where TSystem : class, ISystemBase, new() {

            if (this.world == null) {
                
                SystemGroupRegistryException.Throw();
                
            }
            
            return this.runtimeSystem.Has<TSystem>();

        }

        /// <summary>
        /// Add system by type
        /// Retrieve system from pool, OnConstruct() call
        /// </summary>
        /// <typeparam name="TSystem"></typeparam>
        public bool AddSystem<TSystem>() where TSystem : class, ISystemBase, new() {

            if (this.world == null) {
                
                SystemGroupRegistryException.Throw();
                
            }

            var instance = PoolSystems.Spawn<TSystem>();
            if (this.AddSystem(instance) == false) {

                instance.world = null;
                PoolSystems.Recycle(ref instance);
                return false;

            }

            return true;

        }

        /// <summary>
        /// Add system manually
        /// Pool will not be used, OnConstruct() call
        /// </summary>
        /// <param name="instance"></param>
        public bool AddSystem(ISystemBase instance) {

            if (this.world == null) {
                
                SystemGroupRegistryException.Throw();
                
            }

            WorldUtilities.SetWorld(this.world);
            
            instance.world = this.world;
            if (instance is ISystemValidation instanceValidate) {

                if (instanceValidate.CouldBeAdded() == false) {
                    
                    instance.world = null;
                    return false;
                    
                }

            }

            this.runtimeSystem.Add(instance);
            instance.OnConstruct();

            if (instance is ISystemFilter systemFilter) {

                systemFilter.filter = systemFilter.CreateFilter();

            }

            this.world.UpdateGroup(this);
            
            return true;

        }

        /// <summary>
        /// Remove system manually
        /// Pool will not be used, OnDeconstruct() call
        /// </summary>
        /// <param name="instance"></param>
        public void RemoveSystem(ISystemBase instance) {

            if (this.world == null) {
                
                SystemGroupRegistryException.Throw();
                
            }

            if (this.runtimeSystem.Remove(instance) == true) {

                if (instance is ISystemFilter systemFilter) {

                    systemFilter.filter = Filter.Empty;
                    
                }
                instance.world = null;
                instance.OnDeconstruct();
    
                this.world.UpdateGroup(this);

            }
            
        }

        /// <summary>
        /// Get first system by type
        /// </summary>
        /// <typeparam name="TSystem"></typeparam>
        /// <returns></returns>
        public TSystem GetSystem<TSystem>() where TSystem : class, ISystemBase {

            if (this.world == null) {
                
                SystemGroupRegistryException.Throw();
                
            }

            return this.runtimeSystem.Get<TSystem>();
            
        }

    }

}