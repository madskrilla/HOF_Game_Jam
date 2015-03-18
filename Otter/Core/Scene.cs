﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Otter {
    /// <summary>
    /// Class used to manage Entities. The active Game should update the active Scene, which then updates
    /// all of the contained Entities.
    /// </summary>
    public class Scene {

        #region Private Fields

        List<Entity> entitiesToAdd = new List<Entity>();
        List<Entity> entitiesToRemove = new List<Entity>();
        List<Entity> entitiesToChangeLayer = new List<Entity>();
        List<Entity> entitiesToChangeOrder = new List<Entity>();

        List<int> groupsToPause = new List<int>();
        List<int> groupsToUnpause = new List<int>();

        List<int> pausedGroups = new List<int>();

        List<Graphic> graphics = new List<Graphic>();

        SortedDictionary<int, List<Entity>> orders = new SortedDictionary<int, List<Entity>>();
        SortedDictionary<int, List<Entity>> layers = new SortedDictionary<int, List<Entity>>();

        internal Dictionary<int, List<Collider>> Colliders = new Dictionary<int, List<Collider>>();

        List<Entity> entities = new List<Entity>();

        int entityCount = 0;

        #endregion

        #region Public Fields

        /// <summary>
        /// The Glide instance for this Scene to control all tweens.
        /// </summary>
        public Tweener Tweener = new Tweener();

        /// <summary>
        /// The current time since this Scene has started.
        /// </summary>
        public float Timer;

        /// <summary>
        /// An action that triggers during Update().
        /// </summary>
        public Action OnUpdate = delegate { };

        /// <summary>
        /// An action that triggers during UpdateFirst().
        /// </summary>
        public Action OnUpdateFirst = delegate { };

        /// <summary>
        /// An action that triggers during UpdateLast().
        /// </summary>
        public Action OnUpdateLast = delegate { };

        /// <summary>
        /// An action that triggers during Render(), after all entities have been rendered.
        /// </summary>
        public Action OnRender = delegate { };

        /// <summary>
        /// An action that triggers during Begin().
        /// </summary>
        public Action OnBegin = delegate { };

        /// <summary>
        /// An action that triggers during End().
        /// </summary>
        public Action OnEnd = delegate { };

        /// <summary>
        /// An action that triggers when an entity is Added.
        /// </summary>
        public Action OnAdd = delegate { };

        /// <summary>
        /// An action that triggers when an entity is removed.
        /// </summary>
        public Action OnRemove = delegate { };

        /// <summary>
        /// An action that triggers when the Scene is paused because a Scene is stacked on top of it.
        /// </summary>
        public Action OnPause = delegate { };

        /// <summary>
        /// An action that triggers when the Scene is resumed because the active Scene on top of it was popped.
        /// </summary>
        public Action OnResume = delegate { };

        /// <summary>
        /// An action that triggers after the Scene has updated the camera positions for the Game's Surfaces.
        /// </summary>
        public Action OnCameraUpdate = delegate { };

        /// <summary>
        /// The angle of the camera.
        /// </summary>
        public float CameraAngle;

        /// <summary>
        /// The zoom of the camera.
        /// </summary>
        public float CameraZoom = 1f;

        /// <summary>
        /// The width of the scene.
        /// </summary>
        public int Width;

        /// <summary>
        /// The height of the scene.
        /// </summary>
        public int Height;

        /// <summary>
        /// Determines if the scene will control the game surface's camera.
        /// </summary>
        public bool ApplyCamera = true;

        /// <summary>
        /// A reference back to the current scene being run by the game.
        /// </summary>
        public static Scene Instance;

        /// <summary>
        /// Determines if scenes below this scene on the stack are allowed to render.
        /// </summary>
        public bool DrawScenesBelow = true;

        /// <summary>
        /// The bounds that the camera should be clamped inside.
        /// </summary>
        public Rectangle CameraBounds;

        /// <summary>
        /// Determines if the camera will be clamped inside the CameraBounds rectangle.
        /// </summary>
        public bool UseCameraBounds = false;

        /// <summary>
        /// Determines if the scene will render its graphics or not.
        /// </summary>
        public bool Visible = true;

        #endregion

        #region Public Properties

        /// <summary>
        /// A reference to the Game that owns this Scene.
        /// </summary>
        public Game Game { get; internal set; }

        /// <summary>
        /// The default surface to render the scene's graphics to.  If null then render
        /// to the default game surface.
        /// </summary>
        public Surface Surface {
            get {
                if (Surfaces == null) return null;
                if (Surfaces.Count == 0) return null;
                return Surfaces[Surfaces.Count - 1];
            }
            set {
                Surfaces.Clear();
                Surfaces.Add(value);
            }
        }

        /// <summary>
        /// The list of surfaces the Scene should render to.
        /// </summary>
        public List<Surface> Surfaces { get; private set; }

        /// <summary>
        /// Half of the scene's width.
        /// </summary>
        public float HalfWidth {
            private set { }
            get { return Width / 2; }
        }

        /// <summary>
        /// Half of the scene's height.
        /// </summary>
        public float HalfHeight {
            private set { }
            get { return Height / 2; }
        }

        /// <summary>
        /// A reference to the Input from the Game controlling this scene.
        /// </summary>
        public Input Input {
            get { return Game.Input; }
        }

        /// <summary>
        /// The current number of entities in the scene.
        /// </summary>
        public int EntityCount {
            get { return entityCount; }
        }

        /// <summary>
        /// The current mouse X position in relation to the scene space.
        /// </summary>
        public float MouseX {
            get { return Input.MouseX + CameraX; }
        }

        /// <summary>
        /// The current mouse Y position in relation to the scene space.
        /// </summary>
        public float MouseY {
            get { return Input.MouseY + CameraY; }
        }

        /// <summary>
        /// The current raw mouse X position in relation to the scene space.
        /// </summary>
        public float MouseRawX {
            get { return Input.MouseRawX + CameraX; }
        }

        /// <summary>
        /// The current raw mouse Y position in relation to the scene space.
        /// </summary>
        public float MouseRawY {
            get { return Input.MouseRawY + CameraY; }
        }

        /// <summary>
        /// The X position of the camera in the scene.
        /// </summary>
        public float CameraX {
            get {
                return cameraX;
            }
            set {
                cameraX = value;
                if (UseCameraBounds) {
                    cameraX = Util.Clamp(cameraX, CameraBounds.Left, CameraBounds.Right - CameraWidth);
                }
            }
        }

        /// <summary>
        /// The X position of the center of the camera.
        /// </summary>
        public float CameraCenterX {
            get { return cameraX + Game.HalfWidth; }
        }

        /// <summary>
        /// The Y position of the center of the camera.
        /// </summary>
        public float CameraCenterY {
            get { return cameraY + Game.HalfHeight; }
        }

        /// <summary>
        /// The Y position of the camera in the scene.
        /// </summary>
        public float CameraY {
            get {
                return cameraY;
            }
            set {
                cameraY = value;
                if (UseCameraBounds) {
                    cameraY = Util.Clamp(cameraY, CameraBounds.Top, CameraBounds.Bottom - CameraHeight);
                }
            }
        }

        public float CameraWidth {
            get {
                return Game.Width / CameraZoom;
            }
        }

        public float CameraHeight {
            get {
                return Game.Height / CameraZoom;
            }
        }

        /// <summary>
        /// A reference to the debugger object from the game that owns this scene.
        /// </summary>
        public Debugger Debugger {
            get { return Game.Debugger; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a Scene with a specified width and height. If the width and height are not defined they
        /// will be inferred by the Game class that uses the Scene.
        /// </summary>
        /// <param name="width">The width of the scene.</param>
        /// <param name="height">The height of the scene.</param>
        public Scene(int width = 0, int height = 0) {
            Width = width;
            Height = height;
            Surfaces = new List<Surface>();
            CameraBounds = new Rectangle(0, 0, (int)width, (int)height);
        }

        #endregion

        #region Private Methods

        void RenderScene() {
            if (Visible) {
                foreach (var g in graphics) {
                    g.Render();
                }

                OnRender();
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// A handy shortcut for casting the Scene as a specific scene type.
        /// For some reason I just like this better than doing (Scene as Type).Whatever();
        /// </summary>
        /// <typeparam name="T">The type of scene.</typeparam>
        /// <returns>The scene as that type.</returns>
        public T As<T>() where T : Scene {
            return (T)this;
        }

        /// <summary>
        /// Centers the camera of the scene.
        /// </summary>
        /// <param name="x">The x coordinate to be the center of the scene.</param>
        /// <param name="y">The y coordinate to be the center of the scene.</param>
        public void CenterCamera(float x, float y) {
            CameraX = x - Game.HalfWidth;
            CameraY = y - Game.HalfHeight;
        }

        /// <summary>
        /// Add an entity to the scene.
        /// </summary>
        /// <param name="e">Adds a new entity</param>
        /// <returns>The added Entity.</returns>
        public T Add<T>(T e) where T : Entity {
            if (e == null) throw new ArgumentNullException("Entity cannot be null.");
            if (e.Scene != null) return e;

            entitiesToAdd.Add(e);
            e.Scene = this;
            
            e.MarkedForRemoval = false;
            e.MarkedForAdd = true;

            return e;
        }

        /// <summary>
        /// Create and add a new Entity to the Scene.
        /// </summary>
        /// <typeparam name="T">The Type of entity to add.</typeparam>
        /// <param name="constructorArgs">The constructor arguments for creating the Entity.</param>
        /// <returns>The created Entity.</returns>
        public T Add<T>(params object[] constructorArgs) where T : Entity {
            return Add((T)Activator.CreateInstance(typeof(T), constructorArgs));
        }

        /// <summary>
        /// Add a list of Entities to the scene.
        /// </summary>
        /// <typeparam name="T">The type of Entity.</typeparam>
        /// <param name="entities">The list of Entities.</param>
        /// <returns>The list of Entities.</returns>
        public List<T> Add<T>(List<T> entities) where T : Entity {
            foreach (var e in entities) {
                Add(e);
            }
            return entities;
        }

        /// <summary>
        /// Adds an Entity only if no other Entities of that type exist in the Scene already.
        /// </summary>
        /// <typeparam name="T">The type of Entity.</typeparam>
        /// <param name="e">The Entity to add.</param>
        /// <returns>The added Entity, or the Entity of type T that exists in the Scene already.</returns>
        public T AddUnique<T>(T e) where T : Entity {
            if (GetEntity<T>() == null) {
                if (entitiesToAdd.Count(en => en is T) == 0) {
                    return Add(e);
                }
                else {
                    return (T)entitiesToAdd.Find(en => en is T);
                }
            }
            return GetEntity<T>();
        }

        /// <summary>
        /// Creates an adds an Entity to the Scene if there is no Entity of that type in the Scene already.
        /// </summary>
        /// <typeparam name="T">The type of Entity to create and Add.</typeparam>
        /// <param name="constructorArgs">The constructor arguments for creating the Entity.</param>
        /// <returns>The added Entity, or the Entity of type T that exists in the Scene already.</returns>
        public T AddUnique<T>(params object[] constructorArgs) where T : Entity {
            return AddUnique((T)Activator.CreateInstance(typeof(T), constructorArgs));
        }

        /// <summary>
        /// Adds a list of Entities to the Scene if there is no Entity of that type added already.
        /// </summary>
        /// <typeparam name="T">The type of Entity</typeparam>
        /// <param name="entities">The list of Entities to AddUnique.</param>
        /// <returns>A list of the Entities that were successfully added.</returns>
        public List<T> AddUnique<T>(List<T> entities) where T : Entity {
            var added = new List<T>();
            entities.ForEach(e => {
                if (AddUnique(e) != null) {
                    added.Add(e);
                }
            });
            return added;
        }

        /// <summary>
        /// Add multiple entities to the scene.
        /// </summary>
        /// <param name="entities">The entities to add.</param>
        /// <returns>A list of the entities.</returns>
        public List<Entity> AddMultiple(params Entity[] entities) {
            var r = new List<Entity>();
            foreach (var e in entities) {
                r.Add(Add(e));
            }
            return r;
        }

        /// <summary>
        /// Set the only graphic of the scene.
        /// </summary>
        /// <param name="g">The graphic.</param>
        /// <returns>The graphic.</returns>
        public T SetGraphic<T>(T g) where T : Graphic {
            graphics.Clear();
            graphics.Add(g);
            return g;
        }

        /// <summary>
        /// Adds a graphic to the scene.
        /// </summary>
        /// <param name="g">The graphic.</param>
        /// <returns>The graphic.</returns>
        public T AddGraphic<T>(T g) where T : Graphic {
            graphics.Add(g);
            return g;
        }

        public T AddGraphic<T>(T g, float x, float y) where T : Graphic {
            graphics.Add(g);
            g.SetPosition(x, y);
            return g;
        }

        /// <summary>
        /// Add multiple graphics to the scene.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <returns>A list of the graphics added.</returns>
        public List<Graphic> AddGraphics(params Graphic[] graphics) {
            var r = new List<Graphic>();
            foreach (var g in graphics) {
                r.Add(AddGraphic(g));
            }
            return r;
        }

        /// <summary>
        /// Removes a graphic from the scene.
        /// </summary>
        /// <typeparam name="T">The type (inferred from the parameter.)</typeparam>
        /// <param name="g">The graphic to remove.</param>
        /// <returns>The graphic.</returns>
        public T RemoveGraphic<T>(T g) where T : Graphic {
            graphics.Remove(g);
            return g;
        }

        /// <summary>
        /// Removes all graphics from the scene.
        /// </summary>
        public void ClearGraphics() {
            graphics.Clear();
        }

        /// <summary>
        /// Adds a graphic to the Scene and sets its Scroll value to 0.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="g">The graphic to add.</param>
        /// <returns>The added graphic.</returns>
        public T AddGraphicGUI<T>(T g) where T : Graphic {
            g.Scroll = 0;
            return AddGraphic(g);
        }

        public T AddGraphicGUI<T>(T g, float x, float y) where T : Graphic {
            g.Scroll = 0;
            g.SetPosition(x, y);
            return AddGraphic(g);
        }

        /// <summary>
        /// Adds graphics to the Scene and sets their Scroll values to 0.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="graphics">The graphics to add.</param>
        /// <returns>The added graphics.</returns>
        public List<Graphic> AddGraphicsGUI(params Graphic[] graphics) {
            var r = new List<Graphic>();
            foreach (var g in graphics) {
                r.Add(AddGraphicGUI(g));
            }
            return r;
        }

        /// <summary>
        /// Removes an entity from the scene.
        /// </summary>
        /// <typeparam name="T">The type (inferred from the parameter.)</typeparam>
        /// <param name="e">The entity to remove.</param>
        /// <returns>The entity.</returns>
        public T Remove<T>(T e) where T : Entity {
            if (e == null) throw new ArgumentNullException("Entity to remove cannot be null.");
            if (e.MarkedForRemoval) return e;
            if (e.Scene == null) return e;

            if (!entitiesToAdd.Contains(e)) {
                entitiesToRemove.Add(e); // Only add to entities to remove if it has been added already.
            }

            e.MarkedForRemoval = true;
            e.MarkedForAdd = false;

            return e;
        }

        /// <summary>
        /// Remove all entities from the scene.
        /// </summary>
        public void RemoveAll() {
            foreach (var e in entities) {
                Remove(e);
            }
        }

        /// <summary>
        /// Add a surface to the list of surfaces that the scene should render to.
        /// This only applies to the Scene's graphics, NOT the entities in the scene.
        /// </summary>
        /// <param name="target"></param>
        public void AddSurface(Surface target) {
            if (Surfaces == null) Surfaces = new List<Surface>();
            Surfaces.Add(target);
        }

        /// <summary>
        /// Remove a surface from the list of targets that the scene should render to.
        /// </summary>
        /// <param name="target"></param>
        public void RemoveSurface(Surface target) {
            if (Surfaces == null) Surfaces = new List<Surface>();
            Surfaces.Remove(target);
        }

        /// <summary>
        /// Remove all surface targets and revert back to the default game surface.
        /// </summary>
        public void ClearSurfaces() {
            if (Surfaces == null) Surfaces = new List<Surface>();
            Surfaces.Clear();
        }

        /// <summary>
        /// Called when the scene begins after being switched to, or added to the stack.
        /// </summary>
        public virtual void Begin() {

        }

        /// <summary>
        /// Called when the scene ends after being switched away from, or removed from the stack.
        /// </summary>
        public virtual void End() {

        }

        /// <summary>
        /// Called when the scene is paused because a new scene is stacked on it.
        /// </summary>
        public virtual void Pause() {

        }

        /// <summary>
        /// Called when the scene resumes after a scene is added above it.
        /// </summary>
        public virtual void Resume() {

        }

        /// <summary>
        /// The first update of the scene.
        /// </summary>
        public virtual void UpdateFirst() {

        }

        /// <summary>
        /// The last update of the scene.
        /// </summary>
        public virtual void UpdateLast() {

        }

        /// <summary>
        /// The main update loop of the scene.
        /// </summary>
        public virtual void Update() {

        }

        /// <summary>
        /// Renders the scene.  Graphics added to the scene render first.
        /// Graphics drawn in Render() will render on top of all entities.
        /// </summary>
        public virtual void Render() {

        }

        /// <summary>
        /// Update the internal lists stored by the scene.  The engine will usually take care of this!
        /// </summary>
        public void UpdateLists() {
            while (entitiesToAdd.Count > 0) {
                var adding = new List<Entity>(entitiesToAdd);
                entitiesToAdd.Clear();

                foreach (var e in adding) {
                    if (e.MarkedForRemoval) continue;

                    if (!orders.ContainsKey(e.Order)) {
                        orders.Add(e.Order, new List<Entity>());
                    }
                    orders[e.Order].Add(e);

                    if (!layers.ContainsKey(e.Layer)) {
                        layers.Add(e.Layer, new List<Entity>());
                    }
                    layers[e.Layer].Add(e);

                    entities.Add(e);
                    e.MarkedForAdd = false;

                    foreach (var c in e.Colliders) {
                        AddColliderInternal(c);
                    }

                    entityCount++;
                }

                foreach (var e in adding) {
                    // Invoke these methods after *all* entities in the queue are actually in the scene.
                    e.Added();
                    e.OnAdded();
                    e.UpdateComponentLists(); // Add components after e.Added, so that Entity.Scene is not null for components.
                }
            }

            foreach (var e in entitiesToChangeOrder) {
                orders[e.oldOrder].Remove(e);
                if (orders[e.oldOrder].Count == 0) {
                    orders.Remove(e.oldOrder);
                }
                if (!orders.ContainsKey(e.Order)) {
                    orders.Add(e.Order, new List<Entity>());
                }
                orders[e.Order].Add(e);
            }
            entitiesToChangeOrder.Clear();

            foreach (var e in entitiesToChangeLayer) {
                layers[e.oldLayer].Remove(e);
                if (layers[e.oldLayer].Count == 0) {
                    layers.Remove(e.oldLayer);
                }
                if (!layers.ContainsKey(e.Layer)) {
                    layers.Add(e.Layer, new List<Entity>());
                }
                layers[e.Layer].Add(e);
            }
            entitiesToChangeLayer.Clear();

            while (entitiesToRemove.Count > 0) {
                var removing = new List<Entity>(entitiesToRemove);
                entitiesToRemove.Clear();

                foreach (var e in removing) {
                    orders[e.Order].Remove(e);
                    if (orders[e.Order].Count == 0) {
                        orders.Remove(e.Order);
                    }

                    layers[e.Layer].Remove(e);
                    if (layers[e.Layer].Count == 0) {
                        layers.Remove(e.Layer);
                    }

                    entities.Remove(e);

                    foreach (var c in e.Colliders) {
                        RemoveColliderInternal(c);
                    }

                    entityCount--;
                }

                foreach (var e in removing) {
                    e.Removed();
                    e.OnRemoved();
                    e.Scene = null;
                }
            }
            

            foreach (var group in groupsToPause) {
                if (!pausedGroups.Contains(group)) {
                    foreach (var order in orders) {
                        foreach (var e in order.Value) {
                            if (e.Group == group) {
                                e.Paused();
                            }
                        }
                    }
                    pausedGroups.Add(group);
                }
            }
            groupsToPause.Clear();

            foreach (var group in groupsToUnpause) {
                if (IsGroupPaused(group)) {
                    foreach (var order in orders) {
                        foreach (var e in order.Value) {
                            if (e.Group == group) {
                                e.Resumed();
                            }
                        }
                    }
                    pausedGroups.Remove(group);
                }
            }
            groupsToUnpause.Clear();
        }

        /// <summary>
        /// Tweens a set of numeric properties on an object.
        /// </summary>
        /// <param name="target">The object to tween.</param>
        /// <param name="values">The values to tween to, in an anonymous type ( new { prop1 = 100, prop2 = 0} ).</param>
        /// <param name="duration">Duration of the tween in seconds.</param>
        /// <param name="delay">Delay before the tween starts, in seconds.</param>
        /// <returns>The tween created, for setting properties on.</returns>
        public Tween Tween(object target, object values, float duration, float delay = 0) {
            return Tweener.Tween(target, values, duration, delay);
        }

        /// <summary>
        /// Pause a group of entities.
        /// </summary>
        /// <param name="group">The group to pause.</param>
        public void PauseGroup(int group) {
            if (groupsToUnpause.Contains(group)) {
                groupsToUnpause.Remove(group);
            }
            else {
                groupsToPause.Add(group);
            }
        }

        /// <summary>
        /// Resume a paused group of entities.
        /// </summary>
        /// <param name="group">The group to resume.</param>
        public void ResumeGroup(int group) {
            if (!IsGroupPaused(group)) return;

            if (groupsToPause.Contains(group)) {
                groupsToPause.Remove(group);
            }
            else {
                groupsToUnpause.Add(group);
            }
        }

        /// <summary>
        /// Pause or resume a group of entities. If paused, resume. If running, pause.
        /// </summary>
        /// <param name="group">The group to toggle.</param>
        public void PauseGroupToggle(int group) {
            if (IsGroupPaused(group)) {
                ResumeGroup(group);
            }
            else {
                PauseGroup(group);
            }
        }

        /// <summary>
        /// If a group of entities is currently paused. Note that pausing wont happen until the next update
        /// aftering calling pause.
        /// </summary>
        /// <param name="group">The group to check.</param>
        /// <returns>True if the group is paused.</returns>
        public bool IsGroupPaused(int group) {
            if (groupsToPause.Contains(group)) {
                return true;
            }
            return pausedGroups.Contains(group);
        }

        /// <summary>
        /// Get a list of entities of type T from the Scene.
        /// </summary>
        /// <typeparam name="T">The type of entity to collect.</typeparam>
        /// <returns>A list of entities of type T.</returns>
        public List<T> GetEntities<T>() where T : Entity {
            var list = new List<T>();
            foreach (var e in entities) {
                if (e is T) {
                    list.Add(e as T);
                }
            }
            return list;
        }

        /// <summary>
        /// Get a list of Entities of a type from the Scene.
        /// </summary>
        /// <param name="t">The type of Entity to list.</param>
        /// <returns>A list of Entities of type t.</returns>
        public List<Entity> GetEntities(Type t) {
            return entities.Where(e => e.GetType() == t).ToList<Entity>();
        }

        /// <summary>
        /// Get the first instance of an Entity of type T.
        /// </summary>
        /// <typeparam name="T">The entity type to search for.</typeparam>
        /// <returns>The first entity of that type in the scene.</returns>
        public T GetEntity<T>() where T : Entity {
            foreach (var e in entities) {
                if (e is T) {
                    return (e as T);
                }
            }
            return null;
        }

        /// <summary>
        /// Get a list of Entities that have a Collider that matches a specified tag.
        /// </summary>
        /// <param name="colliderTag">The tag to search for.</param>
        /// <returns>Entities that have a Collider with that tag.</returns>
        public List<Entity> GetEntities(int colliderTag) {
            var list = new List<Entity>();
            GetColliders(colliderTag).ForEach(c => list.Add(c.Entity));
            return list;
        }

        /// <summary>
        /// Get a list of Entities that have a Collider that matches a specified tag.
        /// </summary>
        /// <param name="colliderTag">The tag to search for.</param>
        /// <returns>Entities that have a Collider with that tag.</returns>
        public List<Entity> GetEntities(Enum colliderTag) {
            return GetEntities(Convert.ToInt32(colliderTag));
        }

        /// <summary>
        /// Get a list of Colliders that match a specified tag.
        /// </summary>
        /// <param name="tag">The tag to search for.</param>
        /// <returns>Colliders that have the specified tag.</returns>
        public List<Collider> GetColliders(int tag) {
            return Colliders[tag].ToList<Collider>();
        }

        /// <summary>
        /// Get a list of Colliders that match a specified tag.
        /// </summary>
        /// <param name="tag">The tag to search for.</param>
        /// <returns>Colliders that have the specified tag.</returns>
        public List<Collider> GetColliders(Enum tag) {
            return GetColliders(Convert.ToInt32(tag));
        }

        /// <summary>
        /// Count how many entities of type T are in this Scene.
        /// </summary>
        /// <typeparam name="T">The type of entity to count.</typeparam>
        /// <returns>The number of entities of type T.</returns>
        public int GetCount<T>() where T : Entity {
            var count = 0;
            foreach (var e in entities) {
                if (e is T) {
                    count++;
                }
            }
            return count;
        }

        #endregion

        #region Internal

        internal float
            cameraX,
            cameraY;

        internal void AddColliderInternal(Collider c) {
            foreach (var tag in c.Tags) {
                if (Colliders.ContainsKey(tag)) {
                    if (!Colliders[tag].Contains(c)) { // Quick fix to prevent double adding.
                        Colliders[tag].Add(c);
                    }
                }
                else {
                    Colliders[tag] = new List<Collider>();
                    Colliders[tag].Add(c);
                }
            }
        }

        internal void RemoveColliderInternal(Collider c) {
            foreach (var tag in c.Tags) {
                if (Colliders.ContainsKey(tag)) {
                    Colliders[tag].Remove(c);

                    if (Colliders[tag].Count == 0) {
                        Colliders.Remove(tag);
                    }
                }
            }
        }

        internal void BeginInternal() {
            Instance = this;

            foreach (var e in entitiesToAdd) {
                e.SceneBegin();
            }
            Game.OnSceneBegin();
            OnBegin();

            if (Width == 0 || Height == 0) {
                Width = Game.Width;
                Height = Game.Height;
            }

            Begin();
        }



        internal void EndInternal() {
            foreach (var order in orders) {
                foreach (var e in order.Value) {
                    e.SceneEnd();
                }
            }
            Game.OnSceneEnd();
            OnEnd();
            End();

            UpdateLists(); // Testing this
        }

        internal void PauseInternal() {
            foreach (var order in orders) {
                foreach (var e in order.Value) {
                    e.ScenePause();
                }
            }
            OnPause();

            Pause();
        }

        internal void ResumeInternal() {
            Instance = this;

            foreach (var order in orders) {
                foreach (var e in order.Value) {
                    e.SceneResume();
                }
            }
            OnResume();

            Resume();
        }

        internal void UpdateFirstInternal() {
            OnUpdateFirst();

            UpdateFirst();

            foreach (var order in orders) {
                foreach (var e in order.Value) {
                    if (e.AutoUpdate) {
                        if (!IsGroupPaused(e.Group)) {
                            e.UpdateFirstInternal();
                        }
                    }
                }
            }

        }

        internal void UpdateLastInternal() {
            OnUpdateLast();

            UpdateLast();

            foreach (var order in orders) {
                foreach (var e in order.Value) {
                    if (e.AutoUpdate) {
                        if (!IsGroupPaused(e.Group)) {
                            e.UpdateLastInternal();
                        }
                    }
                    if (e.Order != order.Key) {
                        entitiesToChangeOrder.Add(e);
                        e.oldOrder = order.Key;
                    }
                }
            }

            foreach (var layer in layers.Reverse()) {
                foreach (var e in layer.Value) {
                    if (e.Layer != layer.Key) {
                        entitiesToChangeLayer.Add(e);
                        e.oldLayer = layer.Key;
                    }
                }
            }

            foreach (Graphic g in graphics) {
                g.Update();
            }

            if (UseCameraBounds) {
                CameraX = Util.Clamp(CameraX, CameraBounds.Left, CameraBounds.Right - Game.Width);
                CameraY = Util.Clamp(CameraY, CameraBounds.Top, CameraBounds.Bottom - Game.Height);
            }

            foreach (var g in graphics) {
                g.Update();
            }

            UpdateCamera();

            Timer += Game.DeltaTime;
        }

        internal void UpdateCamera() {
            var cx = CameraX;
            var cy = CameraY;

            if (Debugger.Instance != null) {
                if (Debugger.IsOpen) {
                    cx += Debugger.DebugCameraX;
                    cy += Debugger.DebugCameraY;
                }
            }

            if (ApplyCamera) {
                Game.Surfaces.FindAll(s => s.UseSceneCamera).ForEach(s => s.SetView(Util.Round(cx), Util.Round(cy), CameraAngle, CameraZoom));
                Game.Surface.SetView(Util.Round(cx), Util.Round(cy), CameraAngle, CameraZoom);
            }

            OnCameraUpdate();
        }

        internal void UpdateInternal() {
            OnUpdate();

            Tweener.Update(Game.DeltaTime);

            Update();

            foreach (var order in orders) {
                foreach (var e in order.Value) {
                    if (e.AutoUpdate) {
                        if (!IsGroupPaused(e.Group)) {
                            e.UpdateInternal();
                        }
                    }
                }
            }

        }

        internal void RenderInternal() {
            //Render scene graphics behind everything (Scenery!)
            if (Surface == null) {
                RenderScene();
            }
            else {
                Surface temp = Draw.Target;

                foreach (var surface in Surfaces) {
                    Draw.SetTarget(surface);

                    RenderScene();
                }

                Draw.SetTarget(temp);
            }

            foreach (var layer in layers.Reverse()) {
                foreach (var e in layer.Value) {
                    if (e.AutoRender && e.Visible) {
                        e.RenderInternal();
                    }
                }
            }

            if (Surface == null) {
                Render();
            }
            else {
                Surface temp = Draw.Target;

                foreach (var surface in Surfaces) {
                    Draw.SetTarget(surface);

                    Render();
                }

                Draw.SetTarget(temp);
            }
        }

        #endregion
        
    }
}
